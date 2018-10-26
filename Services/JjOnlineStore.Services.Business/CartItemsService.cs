using AutoMapper;
using Optional;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using JjOnlineStore.Common.BindingModels.CartItems;
using JjOnlineStore.Data.EF;
using JjOnlineStore.Services.Business._Base;
using JjOnlineStore.Services.Core;
using JjOnlineStore.Data.Entities;
using JjOnlineStore.Services.Data.CartItems;
using JjOnlineStore.Common.ViewModels;
using JjOnlineStore.Extensions;

using static JjOnlineStore.Services.Data.ServiceConstants;

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

        public async Task<Option<CartItemServiceModel, Error>> CreateOrError(CartItemBm cartItem)
            => await Exists(cartItem) ?
            Option.None<CartItemServiceModel, Error>(CartItemExistsErrMsg.ToError()) :
            (await CreateAsync(cartItem)).Some<CartItemServiceModel, Error>();

        public async Task<CartItemServiceModel> CreateAsync(CartItemBm cartItem)
        {
            var entity = Mapper.Map<CartItemBm, CartItem>(cartItem);
            await DbContext.CartItems.AddAsync(entity);
            await DbContext.SaveChangesAsync();
            return Mapper.Map<CartItem, CartItemServiceModel>(entity);
        }

        public async Task<bool> Exists(CartItemBm model)
            => await DbContext
            .CartItems
            .AnyAsync(ci => ci.CartId == model.CartId &&
                    ci.ProductId == model.ProductId);

    }
}
