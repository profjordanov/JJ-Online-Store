using System.Threading.Tasks;
using JjOnlineStore.Common.ViewModels.Orders;

namespace JjOnlineStore.Services.Core
{
    public interface IOrdersService
    {
        Task<OrderVm> GetByIdAsync(long orderId);
        Task<long> CreateAsync(OrderVm model);
    }
}