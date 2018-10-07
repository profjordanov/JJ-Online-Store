using System.Collections.Generic;
using System.Threading.Tasks;
using JjOnlineStore.Common.ViewModels;
using JjOnlineStore.Common.ViewModels.Admin.Categories;
using Optional;

namespace JjOnlineStore.Services.Core.Admin
{
    public interface IAdminCategoryService
    {
        Task<IEnumerable<CategoryViewModel>> AllAsync();

        Task<Option<CategoryViewModel, Error>> CreateCategoryAsync(string name);
    }
}