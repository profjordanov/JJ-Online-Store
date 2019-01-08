using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using static JjOnlineStore.Common.GlobalConstants;

namespace JjOnlineStore.Web.Areas.Blog.Controllers
{
    [Area(BlogArea)]
    [Authorize(Roles = AdministratorRoleName)]
    public class ArticlesController : Controller
    {
        /// GET: /Blog/Articles/Index
        /// <summary>
        /// 
        /// </summary>
        [HttpGet]
        public IActionResult Index() => View();
    }
}