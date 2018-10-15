using System.Collections.Generic;
using System.Threading.Tasks;
using JjOnlineStore.Common.ViewModels.Products;

namespace JjOnlineStore.Services.Core.Admin
{
    public interface IAdminProductsService
    {
        Task<IEnumerable<ProductViewModel>> AllWithoutDeletedAsync();

        Task CreateAsync(ProductViewModel model);
    }
}