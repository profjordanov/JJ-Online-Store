using Microsoft.AspNetCore.Mvc;

namespace JjOnlineStore.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public ActionResult<string> Get()
        {
            return "Welcome to JJ Online Store serverless API ! " 
                   + "The API is up and running! "
                   + "Author: Jordan Jordanov";
        }      
    }
}
