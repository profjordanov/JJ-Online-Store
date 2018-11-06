using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using JjOnlineStore.Common.ViewModels.Products;
using JjOnlineStore.Services.Core;
using JjOnlineStore.Common.ViewModels;

using static JjOnlineStore.Common.GlobalConstants;
using static JjOnlineStore.Web.ViewPaths;

namespace JjOnlineStore.Web.Controllers
{
    [Route("[controller]/[action]")]
    public class ProductsController : BaseController
    {
        private readonly IProductsService _productsService;
        public ProductsController(IProductsService productsService)
        {
            _productsService = productsService;
        }

        /// GET: /Products/Index
        /// <summary>
        /// Base products view with model: collection of products.
        /// </summary>
        public async Task<IActionResult> Index(string errorMsg = null)
        {
            if (errorMsg != null)
            {
                TempData[ErrorMessage] = errorMsg;
            }
            return View(await _productsService.AllWithoutDeletedAsync());
        }

        /// GET: /Products/Details?Id
        /// <summary>
        /// Gets product details.
        /// </summary>
        /// <param name="id">Product ID.</param>
        /// <returns>Option of ProductViewModel or Error.</returns>
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            return (await _productsService.GetByIdAsync(id.Value))
                .Match(DisplayDetails, DetailsError);
        }

        /// <summary>
        /// Displays product details.
        /// </summary>
        /// <param name="model">Valid Product Model.</param>
        private IActionResult DisplayDetails(ProductViewModel model)
            => View(ProductsDetails, model);

        /// <summary>
        /// Redirects to /Products/Index 
        /// and displays product details error in fancy-box.
        /// </summary>
        private IActionResult DetailsError(Error error)
            => RedirectToAction(nameof(Index), new { errorMsg = error.ToString() });
    }
}