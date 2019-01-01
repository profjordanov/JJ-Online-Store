using Microsoft.AspNetCore.Mvc;

namespace JjOnlineStore.Web.Controllers
{
    [Route("customer-service/[action]")]
    public class CustomerServiceController : Controller
    {
        public IActionResult Index() =>
            View();
    }
}