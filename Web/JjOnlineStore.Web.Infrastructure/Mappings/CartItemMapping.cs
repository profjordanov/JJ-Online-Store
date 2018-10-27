using AutoMapper;
using JjOnlineStore.Common.BindingModels.CartItems;
using JjOnlineStore.Data.Entities;
using JjOnlineStore.Services.Data.CartItems;

namespace JjOnlineStore.Web.Infrastructure.Mappings
{
    public class CartItemMapping : Profile
    {
        public CartItemMapping()
        {
            CreateMap<CartItemBm, CartItem>(MemberList.Source);
            CreateMap<CartItem, CartItemServiceModel>(MemberList.Destination);
        }
    }
}
