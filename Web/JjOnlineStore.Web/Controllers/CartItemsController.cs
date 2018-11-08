using JjOnlineStore.Common.BindingModels.CartItems;
using JjOnlineStore.Services.Core;
using JjOnlineStore.Services.Data.CartItems;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNet.Identity;

using System.Net;
using System.Threading.Tasks;


namespace JjOnlineStore.Web.Controllers
{
    [Route("[controller]/[action]")]
    public class CartItemsController : BaseController
    {
        private readonly ICartItemsService _cartItemsService;
        public CartItemsController(ICartItemsService cartItemsService)
        {
            _cartItemsService = cartItemsService;
        }

        /// POST: /CartItems/Create
        /// <summary>
        /// Creates a Cart Item by given Product ID and Quantity.
        /// Current cart is taken by User ID.
        /// </summary>
        /// <returns>Content Result.</returns>
        /// <param name="model">Cart Item Binding Model.</param>
        /// <response code="201">A Cart Item was created.</response>
        [HttpPost]
        [ProducesResponseType(typeof(CartItemServiceModel), (int)HttpStatusCode.Created)]
        public async Task<IActionResult> Create([FromBody] CartItemBm model)
        {
            model.UserId = User.Identity.GetUserId();
            return (await _cartItemsService.CreateAsync(model))
                .Match(ci => CreatedAtAction(nameof(Create), ci), ErrorContent);
        }

        [HttpDelete]
        public async Task<IActionResult> FlagDeleted(long cartItemId)
        {
            await _cartItemsService.SetDeletedByIdAsync(cartItemId);
            return Ok();
        }
    }
}