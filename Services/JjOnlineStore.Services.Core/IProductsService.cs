using System.Collections.Generic;
using System.Threading.Tasks;
using JjOnlineStore.Common.ViewModels.Products;

namespace JjOnlineStore.Services.Core
{
    public interface IProductsService
    {
        Task<IEnumerable<ProductViewModel>> AllWithoutDeletedAsync();
    }
}