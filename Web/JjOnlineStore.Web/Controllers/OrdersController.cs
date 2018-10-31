using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using JjOnlineStore.Common.ViewModels.Orders;
using JjOnlineStore.Services.Core;

namespace JjOnlineStore.Web.Controllers
{
    [Authorize]
    [Route("[controller]/[action]")]
    public class OrdersController : BaseController
    {
        private readonly IOrdersService _ordersService;

        public OrdersController(IOrdersService ordersService)
        {
            _ordersService = ordersService;
        }


        /// GET: /Orders/Index
        /// <summary>
        /// Payment and Delivery Methods Screen.
        /// </summary>
        /// <returns></returns>
        public IActionResult Index() =>
            View();

        /// POST: /Orders/Create
        /// <summary>
        /// Creates new Order and Redirects to Confirmation screen.
        /// </summary>
        /// <param name="model">Order View Model.</param>
        /// <returns>ID of the newly created order.</returns>
        [HttpPost] 
        public async Task<IActionResult> Create(OrderVm model) =>
            RedirectToAction(nameof(Confirmation), 
                new { orderId = await _ordersService.CreateAsync(model) });

        ///  GET: /Orders/Confirmation
        ///  <summary>
        ///  Screen for Confirmation of New Order.
        ///  </summary>
        /// <param name="orderId">ID of the Order.</param>
        public async Task<IActionResult> Confirmation(long orderId) =>
            View(await _ordersService.GetByIdAsync(orderId));

        /// GET: /Orders/SuccessfulCheckout
        /// <summary>
        /// Successful Checkout of Order.
        /// </summary>
        public IActionResult SuccessfulCheckout() =>
            View();
    }
}