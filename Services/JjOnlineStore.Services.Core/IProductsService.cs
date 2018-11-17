using System.Collections.Generic;
using System.Threading.Tasks;
using JjOnlineStore.Common.Enumeration;
using Optional;
using JjOnlineStore.Common.ViewModels;
using JjOnlineStore.Common.ViewModels.Products;

namespace JjOnlineStore.Services.Core
{
    public interface IProductsService
    {
        Task<IEnumerable<ProductViewModel>> AllWithoutDeletedAsync();
        Task<IEnumerable<ProductViewModel>> GetByMainCategoryAsync(MainStoreCategories category);
        Task<Option<ProductViewModel, Error>> GetByIdAsync(long id);
    }
}