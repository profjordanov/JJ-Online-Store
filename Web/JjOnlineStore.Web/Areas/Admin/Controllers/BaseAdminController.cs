using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using static JjOnlineStore.Common.GlobalConstants;

namespace JjOnlineStore.Web.Areas.Admin.Controllers
{
    /// <inheritdoc />
    /// <summary>
    /// Only Administrators have access to all controllers inherited from  `BaseAdmin`.
    /// </summary>
    [Area(AdminArea)]
    [Authorize(Roles = AdministratorRoleName)]
    public class BaseAdminController : Controller
    {
        /// <summary>
        /// Admin Dashboard.
        /// </summary>
        public IActionResult BaseIndex() => View();

        /// <summary>
        /// Redirects to /Admin/Categories/All
        /// </summary>
        /// <param name="baseObject">Some object</param>
        protected IActionResult RedirectToCategoryLocal(object baseObject)
            => RedirectToAction(nameof(CategoriesController.All), "Categories", new { Area = AdminArea });
}
}