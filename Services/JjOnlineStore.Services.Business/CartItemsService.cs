using JjOnlineStore.Common.BindingModels.CartItems;
using JjOnlineStore.Data.EF;
using JjOnlineStore.Services.Business._Base;
using JjOnlineStore.Services.Core;
using JjOnlineStore.Data.Entities;
using JjOnlineStore.Services.Data.CartItems;
using JjOnlineStore.Common.ViewModels;
using JjOnlineStore.Extensions;

using AutoMapper;

using Optional;

using Microsoft.EntityFrameworkCore;

using System.Threading.Tasks;

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

        public async Task<Option<CartItemServiceModel, Error>> CreateAsync(CartItemBm model)
        {
            model.CartId = await GetCurrentCartIdByUserId(model.UserId);
            return await ExistsAsync(model)
                ? Option.None<CartItemServiceModel, Error>(CartItemExistsErrMsg.ToError())
                : (await SaveAsync(model)).Some<CartItemServiceModel, Error>();
        }


        private async Task<CartItemServiceModel> SaveAsync(CartItemBm model)
        {
            var entity = Mapper.Map<CartItemBm, CartItem>(model);
            await DbContext.CartItems.AddAsync(entity);
            await DbContext.SaveChangesAsync();
            return Mapper.Map<CartItem, CartItemServiceModel>(entity);
        }

        private async Task<bool> ExistsAsync(CartItemBm model)
            => await DbContext
            .CartItems
            .AnyAsync(ci => ci.CartId == model.CartId &&
                    ci.ProductId == model.ProductId);
    }
}
