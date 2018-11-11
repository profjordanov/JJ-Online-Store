using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JjOnlineStore.Web.Controllers
{
    [Authorize]
    [Route("[controller]/[action]")]
    public class UsersController : BaseController
    {
        public async Task<IActionResult> Profile(string username)
        {
            return View();
        }
    }
}