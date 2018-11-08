using JjOnlineStore.Common.ViewModels;
using JjOnlineStore.Data.Entities;

using Optional;

using System.Threading.Tasks;
using System.Collections.Generic;

namespace JjOnlineStore.Services.Core
{
    public interface IOrderItemsService
    {
        Task<Option<IEnumerable<OrderItem>, Error>> CreateRangeByUserIdAndOrderIdAsync(
            string userId, 
            long orderId);
    }
}