using System.Threading.Tasks;
using JjOnlineStore.Common.ViewModels.Home;
using JjOnlineStore.Services.Core;
using Microsoft.AspNetCore.Mvc;

namespace JjOnlineStore.Web.Controllers
{
    public class HomeController : BaseController
    {
        private readonly IProductsService _productsService;

        public HomeController(IProductsService productsService)
        {
            _productsService = productsService;
        }

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

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

    }
}
