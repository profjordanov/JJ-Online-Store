using System.Collections.Generic;
using System.Threading.Tasks;
using Optional;
using JjOnlineStore.Common.ViewModels;
using JjOnlineStore.Common.ViewModels.Products;

namespace JjOnlineStore.Services.Core
{
    public interface IProductsService
    {
        Task<IEnumerable<ProductViewModel>> AllWithoutDeletedAsync();
        Task<Option<ProductViewModel, Error>> GetByIdAsync(long id);
    }
}