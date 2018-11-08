using JjOnlineStore.Services.Core;
using JjOnlineStore.Common.ViewModels;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNet.Identity;

using System.Threading.Tasks;

using static JjOnlineStore.Common.GlobalConstants;


namespace JjOnlineStore.Web.Controllers
{
    [Authorize]
    [Route("[controller]/[action]")]
    public class ShoppingCartController : BaseController
    {
        private readonly IShoppingCartService _shoppingCartService;

        public ShoppingCartController(IShoppingCartService shoppingCartService)
        {
            _shoppingCartService = shoppingCartService;
        }

        /// GET: /ShoppingCart/Index
        /// <summary>
        /// Displays Shopping Cart page for current user.
        /// </summary>
        /// <returns>Shopping Cart with Ordered Items</returns>
        public async Task<IActionResult> Index() =>
            View(await _shoppingCartService.GetByUserIdAsync(User.Identity.GetUserId()));
    }
}