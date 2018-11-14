using JjOnlineStore.Services.Core;

using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Mvc;

using System.Threading.Tasks;

namespace JjOnlineStore.Web.ViewComponents
{
    public class ShoppingCartViewComponent : ViewComponent
    {
        private readonly IShoppingCartService _shoppingCartService;

        public ShoppingCartViewComponent(IShoppingCartService shoppingCartService)
        {
            _shoppingCartService = shoppingCartService;
        }

        public async Task<IViewComponentResult> InvokeAsync() =>
            View(await _shoppingCartService.GetCartComponentModelAsync(User.Identity.GetUserId()));
    }
}