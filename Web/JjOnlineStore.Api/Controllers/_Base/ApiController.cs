using Microsoft.AspNetCore.Mvc;
using JjOnlineStore.Common.ViewModels;

namespace JjOnlineStore.Api.Controllers._Base
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiController : ControllerBase
    {
        protected IActionResult Error(Error error) =>
            new BadRequestObjectResult(error);
    }
}