using System.Collections.Generic;
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

        private IEnumerable<RegisterViewModel> GetTestData = new List<RegisterViewModel>
        {
            new RegisterViewModel
            {
                Email = "test@mail.com",
                Password = "123123",
                ConfirmPassword = "123123"
            },
            new RegisterViewModel
            {
                Email = "test2@mail.com",
                Password = "123123",
                ConfirmPassword = "123123"
            },
            new RegisterViewModel
            {
                Email = "test3@mail.com",
                Password = "123123",
                ConfirmPassword = "123123"
            }
        };

        private void MockMapper<T, TExpected>(T model, TExpected expected) =>
            _mapperMock.Setup(mapper => mapper
                    .Map<TExpected>(model))
                .Returns(expected);
    }
}