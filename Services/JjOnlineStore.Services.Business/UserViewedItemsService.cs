using System.Collections.Generic;
using System.Linq;
using JjOnlineStore.Data.EF;
using JjOnlineStore.Data.Entities;
using JjOnlineStore.Services.Business._Base;
using JjOnlineStore.Services.Core;
using JjOnlineStore.Common.ViewModels.Products;

using AutoMapper;
using AutoMapper.QueryableExtensions;

using Microsoft.EntityFrameworkCore;

using System.Threading.Tasks;

namespace JjOnlineStore.Services.Business
{
    public class UserViewedItemsService : BaseService, IUserViewedItemsService
    {
        public UserViewedItemsService(JjOnlineStoreDbContext dbContext, IMapper mapper) 
            : base(dbContext)
        {
            Mapper = mapper;
        }

        protected IMapper Mapper { get; set; }

        public IEnumerable<ProductViewModel> GetProductsByUserId(string userId) =>
             DbContext
                .UserViewedItems
                .Where(uvi => uvi.UserId == userId)
                .Include(uvi => uvi.Product)
                .Select(uvi => uvi.Product)
                .ProjectTo<ProductViewModel>(Mapper.ConfigurationProvider);

        public async Task AddAsync(string userId, long productId)
        {
            if (!HasAnyByUserIdAndProductIdAsync(userId,productId)
                .GetAwaiter()
                .GetResult())
            {
                var entity = new UserViewedItem
                {
                    ProductId = productId,
                    UserId = userId
                };

                await DbContext.UserViewedItems.AddAsync(entity);
                await DbContext.SaveChangesAsync();
            }
        }

        private async Task<bool> HasAnyByUserIdAndProductIdAsync(string userId, long productId) =>
            await DbContext
                .UserViewedItems
                .AnyAsync(uvi => uvi.UserId == userId &&
                                 uvi.ProductId == productId);
    }
}