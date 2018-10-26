using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using JjOnlineStore.Services.Core;
using JjOnlineStore.Common.ViewModels;

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

        /// POST: /ShoppingCart/Create
        /// <summary>
        /// Creates new Shopping Cart for current user.
        /// </summary>
        /// <returns>Option of Shopping Cart Id or Error.</returns>
        [HttpPost]
        public async Task<IActionResult> Create()
            => (await _shoppingCartService.CreateByUsernameAsync(User.Identity.Name))
                .Match(IdContent, CreateError);

        /// <summary>
        /// Returns Content Result with Current Shopping Cart ID.
        /// </summary>
        /// <param name="shoppingCartId">ID of Shopping Cart.</param>
        private IActionResult IdContent(long shoppingCartId)
            => Content(shoppingCartId.ToString());

        /// <summary>
        /// Redirects to /--/-- 
        /// and displays "user not found" error in fancybox.
        /// </summary>
        private IActionResult CreateError(Error error)
        {
            TempData[ErrorMessage] = error.ToString();
            return View("ErrorViewForMiisingUser");
        }

    }
}