using System.Threading.Tasks;
using JjOnlineStore.Services.Core;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<IActionResult> Index()
            => View(await _productsService.AllWithoutDeletedAsync());
    }
}