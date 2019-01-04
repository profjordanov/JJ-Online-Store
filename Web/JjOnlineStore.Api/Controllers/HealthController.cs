using Microsoft.AspNetCore.Mvc;
using JjOnlineStore.Api.Controllers._Base;

namespace JjOnlineStore.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HealthController : ApiController
    {
        [HttpGet]
        public ActionResult<string> Get() =>
            "The API is up and running!";
    }
}