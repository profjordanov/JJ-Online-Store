using JjOnlineStore.Common.ViewModels;
using Microsoft.AspNetCore.Mvc;

using static Newtonsoft.Json.JsonConvert;

namespace JjOnlineStore.Web.Controllers
{
    public class BaseController : Controller
    {
        protected IActionResult RedirectToError(Error error)
        {
            var currentError = SerializeObject(error);
            TempData["Error"] = currentError;
            return RedirectToAction(nameof(BaseController.Error), "Base");
        }

        protected IActionResult Error()
        {
            var error = DeserializeObject<Error>(TempData["Error"].ToString());
            return View(error);
        }

        protected IActionResult RedirectToLocal(object baseObject)
            => RedirectToAction(nameof(HomeController.Index), "Home");
    }
}