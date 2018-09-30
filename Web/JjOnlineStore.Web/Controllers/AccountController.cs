using System.Threading.Tasks;
using JjOnlineStore.Common.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using JjOnlineStore.Services.Core;

using static JjOnlineStore.Web.ViewPaths;
using static JjOnlineStore.Common.GlobalConstants;

namespace JjOnlineStore.Web.Controllers
{
    [Route("[controller]/[action]")]
    public class AccountController : BaseController
    {
        private readonly IUsersService _usersService;

        public AccountController(IUsersService usersService)
        {
            _usersService = usersService;
        }

        /// GET: /Account/Login
        /// <summary>
        /// Login.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Login(string returnUrl = null)
        {
            // Clear the existing external cookie to ensure a clean login process
            await this.HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);
            this.ViewData["ReturnUrl"] = returnUrl;
            return this.View();
        }

        /// GET: /Account/Register
        /// <summary>
        /// Register.
        /// </summary>
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
            => View();

        /// POST: /Account/Login
        /// <summary>
        /// Register.
        /// </summary>
        /// <param name="model">The user model.</param>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
            => (await _usersService.Register(model))
                .Match(RedirectToLocal, ErrorRegister);

        /// <summary>
        /// Shows errors from register action in fancybox.
        /// </summary>
        /// <param name="error">Error model</param>
        private IActionResult ErrorRegister(Error error)
        {
            TempData[ErrorMessage] = error.ToString();
            return View(RegisterView);
        }
    }
}