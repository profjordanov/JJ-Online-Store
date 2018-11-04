using AutoMapper;
using Moq;
using Shouldly;
using Xunit;
using AutoFixture.Xunit2;

using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Identity;

using JjOnlineStore.Common.ViewModels.Account;
using JjOnlineStore.Data.Entities;
using JjOnlineStore.Services.Business;
using JjOnlineStore.Services.Core;
using JjOnlineStore.Services.Data.Users;
using JjOnlineStore.Tests.Attributes;

using static JjOnlineStore.Tests.DbContextProvider;

namespace JjOnlineStore.Tests.Services
{
    public class UsersServiceTests
    {
        private readonly UsersService _usersService;
        private readonly Mock<UserManager<ApplicationUser>> _userManagerMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<SignInManager<ApplicationUser>> _signInManagerMock;
        private readonly Mock<IShoppingCartService> _cartServiceMock;

        public UsersServiceTests()
        {
            _userManagerMock = IdentityMocksProvider.GetMockUserManager();
            _mapperMock = new Mock<IMapper>();
            _signInManagerMock = IdentityMocksProvider.GetMockSignInManager();
            _cartServiceMock = new Mock<IShoppingCartService>();

            _usersService = new UsersService(
                GetInMemoryDbContext(),
                _userManagerMock.Object,
                _signInManagerMock.Object,
                _mapperMock.Object,
                _cartServiceMock.Object);
        }

        [Theory]
        [CustomAutoData]
        public async Task Register_Should_Register_User(
            RegisterViewModel model,
            ApplicationUser userToRegister,
            UserServiceModel userToReturn)
        {
            // Arrange
            userToRegister = new ApplicationUser
            {
                UserName = model.Email,
                Email = model.Email
            };

            MockMapper(userToRegister, userToReturn);

            _userManagerMock.Setup(userManager => userManager
                    .CreateAsync(userToRegister, model.Password))
                .ReturnsAsync(IdentityResult.Success);

            // Act
            var result = await _usersService.RegisterAsync(model);

            // Assert
            result.Exists(u => u.Email == userToReturn.Email)
                .ShouldBeTrue();
        }

        [Theory]
        [AutoData]
        public async Task Register_Should_Return_Validation_Errors(
            RegisterViewModel model,
            ApplicationUser userToRegister,
            IdentityError[] expectedErrors)
        {
            // Arrange
            userToRegister = new ApplicationUser()
            {
                UserName = model.Email,
                Email = model.Email
            };

            _userManagerMock.Setup(userManager => userManager
                    .CreateAsync(userToRegister, model.Password))
                .ReturnsAsync(IdentityResult.Failed(expectedErrors));

            // Act
            var result = await _usersService.RegisterAsync(model);

            // Assert
            result.HasValue.ShouldBeFalse();
            result.MatchNone(error => error.Messages
                .ShouldAllBe(message => expectedErrors
                    .Any(expectedError => expectedError.Description == message)));
        }

        private void MockMapper<T, TExpected>(T model, TExpected expected) =>
            _mapperMock.Setup(mapper => mapper
                    .Map<TExpected>(model))
                .Returns(expected);
    }
}