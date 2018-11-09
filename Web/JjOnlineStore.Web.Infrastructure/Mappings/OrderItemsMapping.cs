using AutoMapper;
using JjOnlineStore.Common.ViewModels.OrderItems;
using JjOnlineStore.Data.Entities;

namespace JjOnlineStore.Web.Infrastructure.Mappings
{
    public class OrderItemsMapping : Profile
    {
        public OrderItemsMapping()
        {
            CreateMap<OrderItem, OrderItemVm>(MemberList.Destination);
            CreateMap<CartItem, OrderItem>()
                .ForMember(src => src.Id, opts => opts.Ignore())
                .ForMember(src => src.OrderId, opts => opts.Ignore())
                .ForMember(src => src.Order, opts => opts.Ignore());
        }
    }
}