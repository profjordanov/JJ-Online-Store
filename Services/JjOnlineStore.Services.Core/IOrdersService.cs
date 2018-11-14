using JjOnlineStore.Common.ViewModels;
using JjOnlineStore.Common.ViewModels.Orders;

using Optional;

using System.Threading.Tasks;
using System.Collections.Generic;

namespace JjOnlineStore.Services.Core
{
    public interface IOrdersService
    {
        Task<OrderVm> GetByIdAsync(long orderId);
        Task<Option<long, Error>> CreateAsync(OrderVm model);
        IEnumerable<OrderVm> GetByUserId(string userId);
    }
}