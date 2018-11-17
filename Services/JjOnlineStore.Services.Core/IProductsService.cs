using JjOnlineStore.Common.Enumeration;
using JjOnlineStore.Common.ViewModels;
using JjOnlineStore.Common.ViewModels.Products;

using Optional;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace JjOnlineStore.Services.Core
{
    public interface IProductsService
    {
        Task<IEnumerable<ProductViewModel>> AllWithoutDeletedAsync();

        Task<IEnumerable<ProductViewModel>> GetByMainCategoryAsync(MainStoreCategories category);

        Task<Option<ProductViewModel, Error>> GetByIdAsync(long id);

        Task<IEnumerable<ProductViewModel>> GetByMainCategoryAndWordAsync(
            MainStoreCategories category,
            string searchedWord);
    }
}