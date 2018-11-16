using JjOnlineStore.Common.ViewModels.Products;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace JjOnlineStore.Services.Core
{
    public interface IUserViewedItemsService
    {
        IEnumerable<ProductViewModel> GetProductsByUserId(string userId);
        Task AddAsync(string userId, long productId);
    }
}