using JjOnlineStore.Common.ViewModels.Orders;
using JjOnlineStore.Services.Core;
using JjOnlineStore.Common.ViewModels;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNet.Identity;

using System.Threading.Tasks;

namespace JjOnlineStore.Web.Controllers
{
    [Authorize]
    [Route("[controller]/[action]")]
    public class OrdersController : BaseController
    {
        private readonly IOrdersService _ordersService;
        private readonly ICartItemsService _cartItemsService;

        public OrdersController(IOrdersService ordersService, ICartItemsService cartItemsService)
        {
            _ordersService = ordersService;
            _cartItemsService = cartItemsService;
        }


        /// GET: /Orders/Index
        /// <summary>
        /// Payment and Delivery Methods Screens.
        /// </summary>
        public IActionResult Index() =>
            View();

        /// POST: /Orders/Create
        /// <summary>
        /// Creates new Order and Redirects to Confirmation screen.
        /// </summary>
        /// <param name="model">Order View Model.</param>
        /// <returns>Either ID of the newly created order or Error.</returns>
        [HttpPost]
        public async Task<IActionResult> Create(OrderVm model)
        {
            model.UserId = User.Identity.GetUserId();
            return (await _ordersService.CreateAsync(model))
                .Match(RedirectToConfirmation, RedirectToIndexWithErrMsg);
        }

        /// <summary>
        /// Redirects To Confirmation Page.
        /// </summary>
        public IActionResult RedirectToConfirmation(long orderId) =>
            RedirectToAction(nameof(Confirmation), new {orderId});

        /// <summary>
        /// Redirects To Index Page With Error Message for Order Creation. 
        /// </summary>
        public IActionResult RedirectToIndexWithErrMsg(Error error) =>
            RedirectToAction(nameof(Index));


        ///  GET: /Orders/Confirmation
        ///  <summary>
        ///  Screen for Confirmation of the new Order.
        ///  </summary>
        /// <param name="orderId">ID of the Order.</param>
        public async Task<IActionResult> Confirmation(long orderId) =>
            View(await _ordersService.GetByIdAsync(orderId));

        /// GET: /Orders/SuccessfulCheckout
        /// <summary>
        /// - First erase all current cart items.
        /// - Then display 'Successful Checkout' page.
        /// </summary>
        public async Task<IActionResult> SuccessfulCheckout()
        {
            await _cartItemsService.DeleteAllInCartByUserIdAsync(User.Identity.GetUserId());
            return View();
        }
    }
}