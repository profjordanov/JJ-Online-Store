using JjOnlineStore.Services.Core;
using JjOnlineStore.Common.ViewModels;
using JjOnlineStore.Common.ViewModels.Account;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;

using System.Threading.Tasks;

using static JjOnlineStore.Web.ViewPaths;
using static JjOnlineStore.Common.GlobalConstants;

namespace JjOnlineStore.Web.Controllers
{
    /// <summary>
    /// User logging and registration .
    /// </summary>
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
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        /// POST: /Account/Login
        /// <summary>
        /// Login.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Login(CredentialsModel model)
            => (await _usersService.LoginAsync(model))
                .Match(RedirectToLocal, ErrorLogin);

        /// <summary>
        /// Shows possible errors from login action in fancy-box.
        /// </summary>
        /// <param name="error">Error model.</param>
        private IActionResult ErrorLogin(Error error)
        {
            TempData[ErrorMessage] = error.ToString();
            return View(LoginView);
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
        /// Register new user and create it's shopping cart.
        /// </summary>
        /// <param name="model">The user model.</param>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
            => (await _usersService.RegisterAsync(model))
                .Match(RedirectToLocal, ErrorRegister);

        /// <summary>
        /// Shows errors from register action in fancy-box.
        /// </summary>
        /// <param name="error">Error model.</param>
        public IActionResult ErrorRegister(Error error)
        {
            TempData[ErrorMessage] = error.ToString();
            return View(RegisterView);
        }

        /// <summary>
        /// Logout.
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _usersService.SignOutAsync();
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }
    }
}