using Optional;
using System.Threading.Tasks;
using JjOnlineStore.Common.ViewModels;
using JjOnlineStore.Data.EF;
using JjOnlineStore.Data.Entities;
using JjOnlineStore.Extensions;
using JjOnlineStore.Services.Business._Base;
using JjOnlineStore.Services.Core;

namespace JjOnlineStore.Services.Business
{
    public class ShoppingCartService : BaseService, IShoppingCartService
    {
        public ShoppingCartService(JjOnlineStoreDbContext dbContext) 
            : base(dbContext)
        {
        }

        public async Task<Option<long, Error>> CreateByUsernameAsync(string username)
            => await GetUserByNameOrError(username)
            .MapAsync(async author =>
            {
                return await CreateByUserAsync(author);
            });

        public async Task<long> CreateByUserAsync(ApplicationUser user)
        {
            var entity = new Cart
            {
                User = user,
                UserId = user.Id
            };

            await DbContext.Carts.AddAsync(entity);
            await DbContext.SaveChangesAsync();

            return entity.Id;
        }
    }
}
