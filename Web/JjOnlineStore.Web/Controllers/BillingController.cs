using System.Threading.Tasks;
using JjOnlineStore.Services.Core;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JjOnlineStore.Web.Controllers
{
    [Authorize]
    [Route("[controller]/[action]")]
    public class BillingController :  BaseController
    {
        private readonly IOrdersService _ordersService;
        private readonly IBillingService _billingService;

        public BillingController(IOrdersService ordersService, IBillingService billingService)
        {
            _ordersService = ordersService;
            _billingService = billingService;
        }

        public IActionResult Index() =>
            View(_ordersService.GetByUserId(User.Identity.GetUserId()));

        public async Task<IActionResult> CreateInvoice(long orderId)
        {
            await _billingService.CreateInvoiceByOrderIdAsync(orderId);
            return RedirectToAction(nameof(Index));
        }
    }
}