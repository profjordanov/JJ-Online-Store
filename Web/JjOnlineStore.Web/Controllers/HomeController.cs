using JjOnlineStore.Common.ViewModels.Home;
using JjOnlineStore.Services.Core;

using Microsoft.AspNetCore.Mvc;

using System.Threading.Tasks;

namespace JjOnlineStore.Web.Controllers
{
    [Route("[controller]/[action]")]
    public class HomeController : BaseController
    {
        private readonly IProductsService _productsService;

        public HomeController(IProductsService productsService)
        {
            _productsService = productsService;
        }

        /// GET: /Home/Index
        /// <summary>
        /// Home Page.
        /// </summary>
        public async Task<IActionResult> Index()
        {
            var model = new HomeIndexVm
            {
                NewItems = await _productsService.AllWithoutDeletedAsync(),
                OnSaleItems = await _productsService.AllWithoutDeletedAsync(),
                TopSellingItems = await _productsService.AllWithoutDeletedAsync()
            };

            return View(model);
        }
    }
}
