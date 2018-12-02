using System;
using System.Linq;
using AutoMapper;
using Moq;
using Shouldly;
using Xunit;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Identity;

using JjOnlineStore.Common.ViewModels.Account;
using JjOnlineStore.Data.EF;
using JjOnlineStore.Data.Entities;
using JjOnlineStore.Services.Business;
using JjOnlineStore.Services.Core;
using JjOnlineStore.Services.Data.Users;
using JjOnlineStore.Tests.Attributes;

using static JjOnlineStore.Tests.DbContextProvider;

namespace JjOnlineStore.Tests.Services
{
    public class UsersServiceTests : IDisposable
    {
        private readonly UsersService _usersService;
        private readonly Mock<UserManager<ApplicationUser>> _userManagerMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<SignInManager<ApplicationUser>> _signInManagerMock;
        private readonly Mock<IShoppingCartService> _cartServiceMock;
        private readonly JjOnlineStoreDbContext _dbContext;

        public UsersServiceTests()
        {
            _dbContext = GetInMemoryDbContext();
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
        public async Task Login_Should_Work(
            CredentialsModel model, 
            ApplicationUser expectedUser)
        {
            // Arrange
            AddUserWithEmail(model.Email, expectedUser);

            MockCheckPassword(model.Password, true);

            _mapperMock.Setup(mapper => mapper
                .Map<UserServiceModel>(It.IsAny<ApplicationUser>()))
                .Returns(new UserServiceModel());

            // Act
            var result = await _usersService.LoginAsync(model);

        }

        [Theory]
        [CustomAutoData]
        public async Task Login_Should_Return_Exception_When_Credentials_Are_Invalid(
            CredentialsModel model,
            ApplicationUser expectedUser)
        {
            // Arrange
            AddUserWithEmail(model.Email, expectedUser);

            MockCheckPassword(model.Password, false);

            // Act
            var result = await _usersService.LoginAsync(model);

            // Assert
            result.HasValue.ShouldBeFalse();
            result.MatchNone(error => error.Messages?.Count.ShouldBeGreaterThan(0));
        }

        [Theory]
        [CustomAutoData]
        public async Task Register_Should_Register_User(
            RegisterViewModel model,
            ApplicationUser userToRegister,
            UserServiceModel userToReturn)
        {
            MockMapper(model, userToRegister);

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
        [CustomAutoData]
        public async Task Register_Should_Return_Validation_Errors(
            RegisterViewModel model,
            ApplicationUser userToRegister,
            IdentityError[] expectedErrors)
        {
            // Arrange
            MockMapper(model, userToRegister);

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

        private void MockCheckPassword(string password, bool result) =>
            _userManagerMock.Setup(userManager => userManager
                    .CheckPasswordAsync(It.IsAny<ApplicationUser>(), password))
                .ReturnsAsync(result);

        private void AddUserWithEmail(string email, ApplicationUser expected)
        {
            expected.Email = email;
            _dbContext.Users.Add(expected);
            _dbContext.SaveChanges();
        }

        public void Dispose()
        {
            _dbContext.Database.EnsureDeleted();
        }
    }
}