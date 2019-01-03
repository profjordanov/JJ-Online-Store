using Microsoft.AspNetCore.Mvc;
using JjOnlineStore.Common.ViewModels;

namespace JjOnlineStore.Api.Controllers._Base
{
    [Route("api/[controller]")]
    public class ApiController : Controller
    {
        protected IActionResult Error(Error error) =>
            new BadRequestObjectResult(error);
    }
}