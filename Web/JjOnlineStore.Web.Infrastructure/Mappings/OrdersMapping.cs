using AutoMapper;
using JjOnlineStore.Common.ViewModels.Orders;
using JjOnlineStore.Data.Entities;

namespace JjOnlineStore.Web.Infrastructure.Mappings
{
    public class OrdersMapping : Profile
    {
        public OrdersMapping()
        {
            CreateMap<Order, OrderVm>(MemberList.Destination)
                .AfterMap((src, dest) => dest.DecryptSensitiveData(dest));
            CreateMap<OrderVm, Order>(MemberList.Source)
                .BeforeMap((src, dest) => src.EncryptSensitiveData(src));
        }
    }
}