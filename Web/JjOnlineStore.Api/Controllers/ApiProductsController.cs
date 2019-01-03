using JjOnlineStore.Api.Controllers._Base;
using JjOnlineStore.Common.ViewModels.Products;
using JjOnlineStore.Services.Core;

using Microsoft.AspNetCore.Mvc;

using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace JjOnlineStore.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/products/[action]")]
    public class ApiProductsController : ApiController
    {
        private readonly IProductsService _productsService;

        public ApiProductsController(IProductsService productsService)
        {
            _productsService = productsService;
        }

        /// <summary>
        /// Returns all products. 
        /// </summary>
        /// <returns>A collection of products.</returns>
        /// <response code="200"></response>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ProductViewModel>), (int) HttpStatusCode.OK)]
        public async Task<IActionResult> GetAll()
        {
            var result = await _productsService.AllWithoutDeletedAsync();
            return Ok(result);
        }
    }
}