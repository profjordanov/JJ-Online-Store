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
    }
}