using System.Collections.Generic;
using Optional;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using JjOnlineStore.Common.ViewModels;
using JjOnlineStore.Common.ViewModels.CartItems;
using JjOnlineStore.Common.ViewModels.Products;
using JjOnlineStore.Common.ViewModels.ShoppingCarts;
using JjOnlineStore.Data.EF;
using JjOnlineStore.Data.Entities;
using JjOnlineStore.Extensions;
using JjOnlineStore.Services.Business._Base;
using JjOnlineStore.Services.Core;


namespace JjOnlineStore.Services.Business
{
    public class ShoppingCartService : BaseService, IShoppingCartService
    {
        public ShoppingCartService(JjOnlineStoreDbContext dbContext, IMapper mapper) 
            : base(dbContext)
        {
            Mapper = mapper;
        }

        protected IMapper Mapper { get; }

        public async Task<CartVm> GetById(long shoppingCartId)
        {
            var entity = await DbContext
                .Carts
                .Where(sc => sc.Id == shoppingCartId)
                .Include(sc => sc.OrderedItems)
                .FirstOrDefaultAsync();

            var viewModel = Mapper.Map<Cart, CartVm>(entity);
            viewModel.CartItems =
                Mapper.Map<IEnumerable<CartItem>, IEnumerable<CartItemVm>>(entity.OrderedItems)
                    .ToArray();

            foreach (var cartItem in viewModel.CartItems)
            {
                cartItem.Product = await DbContext
                    .Products
                    .Where(p => p.Id == cartItem.ProductId)
                    .ProjectTo<ProductViewModel>(Mapper.ConfigurationProvider)
                    .FirstOrDefaultAsync();
            }
            viewModel.TotalSum =
                viewModel.CartItems.Sum(oi => oi.Product.Price * oi.Quantity);

            return viewModel;
        }


        public async Task<Option<long, Error>> CreateByUsernameAsync(string username)
            => await GetUserByNameOrError(username)
            .MapAsync(async author => await CreateByUserAsync(author));

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
