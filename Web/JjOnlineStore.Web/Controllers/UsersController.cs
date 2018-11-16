using JjOnlineStore.Common.ViewModels.Users;
using JjOnlineStore.Services.Core;

using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JjOnlineStore.Web.Controllers
{
    [Authorize]
    [Route("[controller]/[action]")]
    public class UsersController : BaseController
    {
        private readonly IUserViewedItemsService _viewedItemsService;

        public UsersController(IUserViewedItemsService viewedItemsService)
        {
            _viewedItemsService = viewedItemsService;
        }

        /// GET: /Users/Profile
        /// <summary>
        /// Displays User Profile Page.
        /// </summary>
        /// <returns>
        /// 1) User Recently Viewed Items
        /// </returns>
        public IActionResult Profile() =>
            View(new UserProfileOverviewVm
            {
                Products = _viewedItemsService.GetProductsByUserId(User.Identity.GetUserId())
            });
    }
}