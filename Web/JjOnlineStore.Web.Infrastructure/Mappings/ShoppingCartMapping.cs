using AutoMapper;
using JjOnlineStore.Common.ViewModels.ShoppingCarts;
using JjOnlineStore.Data.Entities;

namespace JjOnlineStore.Web.Infrastructure.Mappings
{
    public class ShoppingCartMapping : Profile
    {
        public ShoppingCartMapping()
        {
            CreateMap<Cart, CartVm>(MemberList.Destination);
        }
    }
}