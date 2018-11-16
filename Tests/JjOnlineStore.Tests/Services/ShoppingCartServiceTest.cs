using System;
using System.Collections.Generic;
using AutoMapper;
using JjOnlineStore.Data.EF;
using JjOnlineStore.Data.Entities;
using JjOnlineStore.Services.Business;
using Moq;
using Xunit;

namespace JjOnlineStore.Tests.Services
{
    public class ShoppingCartServiceTest
    {
        private readonly ShoppingCartService _shoppingCartService;

        private readonly Mock<IMapper> _mapperMock;
        private JjOnlineStoreDbContext _dbContext;

        public ShoppingCartServiceTest()
        {
            _dbContext = DbContextProvider.GetInMemoryDbContext();
            _mapperMock = new Mock<IMapper>();
            _shoppingCartService = GetShoppingCartServiceWithTestData();
        }

        [Fact]
        public void Test()
        {

        }

        private ShoppingCartService GetShoppingCartServiceWithTestData()
        {
            var dbContext = DbContextProvider.GetInMemoryDbContext();
            PopulateData(dbContext);
            return new ShoppingCartService(dbContext,_mapperMock.Object);
        }

        private void PopulateData(JjOnlineStoreDbContext dbContext)
        {
            dbContext.Users.AddRange(GetTestUser());
            dbContext.SaveChanges();
        }

        private IEnumerable<ApplicationUser> GetTestUser() => new List<ApplicationUser>
        {
            new ApplicationUser()
            {
                Id = Guid.NewGuid().ToString(),
                Email = "test@mail.com",
                UserName = "test@mail.com",
                PasswordHash = "xxx",
                NormalizedUserName = "test@mail.com".ToUpper(),
                NormalizedEmail = "test@mail.com".ToUpper(),
                CreatedOn = DateTime.UtcNow
            }
        };
    }
}