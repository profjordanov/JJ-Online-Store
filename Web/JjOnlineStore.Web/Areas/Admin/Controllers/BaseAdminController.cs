using Microsoft.AspNetCore.Mvc;

using static JjOnlineStore.Common.GlobalConstants;

namespace JjOnlineStore.Web.Areas.Admin.Controllers
{
    [Area(AdminArea)]
    public class BaseAdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Redirects to /Admin/Categories/All
        /// </summary>
        /// <param name="baseObject">Some object</param>
        protected IActionResult RedirectToCategoryLocal(object baseObject)
            => RedirectToAction(nameof(CategoriesController.All), "Categories", new { Area = AdminArea });
}
}