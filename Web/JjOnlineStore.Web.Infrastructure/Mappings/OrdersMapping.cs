using AutoMapper;
using JjOnlineStore.Common.ViewModels.Orders;
using JjOnlineStore.Data.Entities;

namespace JjOnlineStore.Web.Infrastructure.Mappings
{
    public class OrdersMapping : Profile
    {
        public OrdersMapping()
        {
            //TODO: DecryptSensitiveData
            CreateMap<Order, OrderVm>(MemberList.Destination)
                .AfterMap((entityModel, viewModel) => viewModel.DecryptSensitiveData(viewModel));
            CreateMap<OrderVm, Order>(MemberList.Source)
                .BeforeMap((viewModel, entityModel) => viewModel.EncryptSensitiveData(viewModel));
        }
    }
}