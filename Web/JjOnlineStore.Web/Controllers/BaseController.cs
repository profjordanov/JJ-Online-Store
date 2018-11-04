using JjOnlineStore.Common.ViewModels;

using Microsoft.AspNetCore.Mvc;

namespace JjOnlineStore.Web.Controllers
{
    public class BaseController : Controller
    {
        /// <summary>
        /// Redirects to Home/Index.
        /// </summary>
        /// <param name="baseObject">Some object</param>
        protected IActionResult RedirectToLocal(object baseObject)
            => RedirectToAction(nameof(HomeController.Index), "Home");

        /// <summary>
        /// Error messages split by ';'.
        /// </summary>
        /// <param name="error">Error struct.</param>
        protected IActionResult ErrorContent(Error error) =>
            Content(error.ToString());
    }
}