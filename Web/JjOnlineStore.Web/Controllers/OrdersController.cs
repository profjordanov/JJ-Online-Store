using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using JjOnlineStore.Common.ViewModels.Orders;

namespace JjOnlineStore.Web.Controllers
{
    [Authorize]
    [Route("[controller]/[action]")]
    public class OrdersController : BaseController
    {
        /// GET: /Orders/Index
        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            return View();
        }

        /// POST: /Orders/Create
        /// <summary>
        ///
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Create(OrderVm model)
        {
            var oreder = model;
            return null;
        }

        /// GET: /Orders/Confirmation
        /// <summary>
        ///
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public IActionResult Confirmation(OrderVm model)
        {
            return View(model);
        }

        /// GET: /Orders/SuccessfulCheckout
        /// <summary>
        ///
        /// </summary>
        public IActionResult SuccessfulCheckout()
        {
            return View();
        }
    }
}