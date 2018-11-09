using JjOnlineStore.Common.ViewModels;
using JjOnlineStore.Common.ViewModels.Orders;

using System.Threading.Tasks;

using Optional;

namespace JjOnlineStore.Services.Core
{
    public interface IOrdersService
    {
        Task<OrderVm> GetByIdAsync(long orderId);
        Task<Option<long, Error>> CreateAsync(OrderVm model);
    }
}