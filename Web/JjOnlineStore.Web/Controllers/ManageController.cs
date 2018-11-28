using JjOnlineStore.Common.ViewModels;
using JjOnlineStore.Common.ViewModels.Manage;
using JjOnlineStore.Services.Core;

using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using System.Threading.Tasks;

using static JjOnlineStore.Web.ViewPaths;
using static JjOnlineStore.Common.GlobalConstants;

namespace JjOnlineStore.Web.Controllers
{
    [Authorize]
    [Route("[controller]/[action]")]
    public class ManageController : BaseController
    {
        private readonly IManageService _manageService;

        public ManageController(IManageService manageService)
        {
            _manageService = manageService;
        }


        /// GET: /Manage/ChangePassword
        /// <summary>
        /// Change User's Password.
        /// </summary>
        [HttpGet]
        public IActionResult ChangePassword() =>
            View();

        /// POST: /Manage/ChangePassword
        /// <summary>
        /// Change User's Password.
        /// </summary>
        /// <param name="model">Change Password View Model</param>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(ChangePasswordVm model)
        {
            model.UserId = User.Identity.GetUserId();
            return (await _manageService.ChangePasswordAsync(model))
                .Match(RedirectToLocal, ChangePasswordError);
        }

        /// <summary>
        /// Change Password Error Messages.
        /// </summary>
        public IActionResult ChangePasswordError(Error error)
        {
            TempData[ErrorMessage] = error.ToString();
            return View(ChangePasswordView);
        }
    }
}