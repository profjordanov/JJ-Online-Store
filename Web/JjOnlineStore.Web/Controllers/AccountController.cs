using System.Threading.Tasks;
using JjOnlineStore.Common.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using JjOnlineStore.Services.Core;
using JjOnlineStore.Services.Data;

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
        public IActionResult Register(string returnUrl = null)
        {
            this.ViewData["ReturnUrl"] = returnUrl;
            return this.View();
        }

        /// POST: /Account/Login
        /// <summary>
        /// Register.
        /// </summary>
        /// <param name="model">The user model.</param>
        /// <param name="returnUrl">Return Url</param>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model, string returnUrl = null)
            => (await _usersService.Register(model))
                .Match(RedirectToLocal, Error);
    }
}