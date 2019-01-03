using JjOnlineStore.Common.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace JjOnlineStore.Api.Controllers._Base
{
    [Route("api/[controller]")]
    public class ApiController : Controller
    {
        protected IActionResult Error(Error error) =>
            new BadRequestObjectResult(error);
    }
}