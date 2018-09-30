using Microsoft.AspNetCore.Identity;
using JjOnlineStore.Data.Entities;
using Moq;

namespace JjOnlineStore.Tests
{
    public static class IdentityMocksProvider
    {
        public static Mock<UserManager<ApplicationUser>> GetMockUserManager()
        {
            var userStoreMock = new Mock<IUserStore<ApplicationUser>>();
            return new Mock<UserManager<ApplicationUser>>(
                userStoreMock.Object, null, null, null, null, null, null, null, null);
        }
    }
}