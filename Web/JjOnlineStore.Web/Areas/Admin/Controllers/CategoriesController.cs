using System.Threading.Tasks;
using JjOnlineStore.Common.ViewModels;
using Microsoft.AspNetCore.Mvc;
using JjOnlineStore.Common.ViewModels.Admin.Categories;
using JjOnlineStore.Services.Core.Admin;

using static JjOnlineStore.Web.ViewPaths;
using static JjOnlineStore.Common.GlobalConstants;

namespace JjOnlineStore.Web.Areas.Admin.Controllers
{
    public class CategoriesController : BaseAdminController
    {
        private readonly IAdminCategoryService _adminCategoryService;
        public CategoriesController(IAdminCategoryService adminCategoryService)
        {
            _adminCategoryService = adminCategoryService;
        }

        /// GET: /Admin/Categories/All
        /// <summary>
        /// All categories.
        /// </summary>
        public async Task<IActionResult> All()
            => View(await _adminCategoryService.AllAsync());

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
            => (await _adminCategoryService.CreateCategoryAsync(model.Name))
                .Match(RedirectToCategoryLocal, ErrorCreate);

        /// <summary>
        /// Shows errors from create action in fancybox.
        /// </summary>
        /// <param name="error">Error model.</param>
        private IActionResult ErrorCreate(Error error)
        {
            TempData[ErrorMessage] = error.ToString();
            return View(CreateCategoryView);
        }
    }
}