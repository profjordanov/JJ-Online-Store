using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using JjOnlineStore.Common.ViewModels;
using JjOnlineStore.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace JjOnlineStore.Web.Controllers
{
    public class BaseController : Controller
    {
        protected IActionResult Error(Error error)
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        protected IActionResult RedirectToLocal(object baseObject)
            => RedirectToAction(nameof(HomeController.Index), "Home");
    }
}