using System.Linq;
using System.Threading.Tasks;
using AutoFixture.Xunit2;
using AutoMapper;
using JjOnlineStore.Common.ViewModels;
using JjOnlineStore.Data.Entities;
using JjOnlineStore.Services.Business;
using Microsoft.AspNetCore.Identity;
using Moq;
using Shouldly;
using Xunit;

namespace JjOnlineStore.Tests.Services
{
    public class UsersServiceTests
    {
        private readonly UsersService _usersService;
        private readonly Mock<UserManager<ApplicationUser>> _userManagerMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<SignInManager<ApplicationUser>> _signInManagerMock;

        public UsersServiceTests()
        {
            _userManagerMock = IdentityMocksProvider.GetMockUserManager();
            _mapperMock = new Mock<IMapper>();
            _signInManagerMock = IdentityMocksProvider.GetMockSignInManager();

            _usersService = new UsersService(
                _userManagerMock.Object,
                _signInManagerMock.Object,
                _mapperMock.Object);
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
            var result = await _usersService.Register(model);

            // Assert
            result.HasValue.ShouldBeFalse();
            result.MatchNone(error => error.Messages
                .ShouldAllBe(message => expectedErrors
                    .Any(expectedError => expectedError.Description == message)));
        }
    }
}