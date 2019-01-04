using System.Linq;
using JjOnlineStore.Common.ViewModels.Home;
using JjOnlineStore.Services.Core;

using Microsoft.AspNetCore.Mvc;

using System.Threading.Tasks;
using JjOnlineStore.Extensions;
using Microsoft.AspNetCore.Authorization;

namespace JjOnlineStore.Web.Controllers
{
    [AllowAnonymous]
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
            var products = await _productsService.AllWithoutDeletedAsync();

            var productsArr = products.ToArray();

            var model = new HomeIndexVm
            {
                NewItems = productsArr.Take(5),
                OnSaleItems = productsArr.Skip(3).Take(3),
                TopSellingItems = productsArr.Skip(6).Take(3)
            };

            return View(model);
        }

        /// GET: /Home/Error
        /// <summary>
        /// Error page.
        /// </summary>
        public IActionResult Error() =>
            View("Don't worry, we will test it in production!".ToError());
    }
}
