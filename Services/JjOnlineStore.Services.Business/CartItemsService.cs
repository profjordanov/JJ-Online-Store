using AutoMapper;
using System.Threading.Tasks;
using JjOnlineStore.Common.BindingModels.CartItems;
using JjOnlineStore.Data.EF;
using JjOnlineStore.Services.Business._Base;
using JjOnlineStore.Services.Core;
using JjOnlineStore.Data.Entities;
using JjOnlineStore.Services.Data.CartItems;


namespace JjOnlineStore.Services.Business
{
    public class CartItemsService : BaseService, ICartItemsService
    {
        public CartItemsService(JjOnlineStoreDbContext dbContext, IMapper mapper) 
            : base(dbContext)
        {
            Mapper = mapper;
        }

        protected IMapper Mapper { get; }

        public async Task<CartItemServiceModel> CreateAsync(CartItemBm cartItem)
        {
            var entity = Mapper.Map<CartItemBm, CartItem>(cartItem);
            await DbContext.CartItems.AddAsync(entity);
            await DbContext.SaveChangesAsync();
            return Mapper.Map<CartItem, CartItemServiceModel>(entity);
        }
    }
}
