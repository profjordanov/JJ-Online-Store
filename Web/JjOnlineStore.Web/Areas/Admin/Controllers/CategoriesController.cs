using System.Threading.Tasks;
using JjOnlineStore.Common.ViewModels.Admin.Categories;
using Microsoft.AspNetCore.Mvc;

namespace JjOnlineStore.Web.Areas.Admin.Controllers
{
    public class CategoriesController : BaseAdminController
    {
        /// GET: /Admin/Categories/Create
        /// <summary>
        /// Creates a category.
        /// </summary>
        public IActionResult Create()
            => View();

        /// POST: /Admin/Categories/Create
        /// <summary>
        /// Creates a category.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Create(CategoryViewModel model)
        {
            var model2 = model;
            return null;
        }
    }
}