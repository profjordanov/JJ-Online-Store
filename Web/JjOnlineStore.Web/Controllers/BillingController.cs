using JjOnlineStore.Services.Core;

using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using System.Threading.Tasks;

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

        /// GET: /Billing/Index
        /// <summary>
        /// 
        /// </summary>
        /// <returns>All orders for current User.</returns>
        public IActionResult Index() =>
            View(_ordersService.GetByUserId(User.Identity.GetUserId()));

        /// GET: /Billing/CreateInvoice
        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderId">Order ID.</param>
        public async Task<IActionResult> CreateInvoice(long orderId)
        {
            await _billingService.CreateInvoiceByOrderIdAsync(orderId);
            return RedirectToAction(nameof(Index));
        }

        /// GET: /Billing/CreateInvoice
        /// <summary>
        /// 
        /// </summary>
        /// <param name="invoiceId">Invoice ID.</param>
        /// <returns>PDF Invoice.</returns>
        public async Task<IActionResult> DownloadInvoice(long invoiceId)
        {
            var invoice = await _billingService.GetPdfInvoiceAsync(invoiceId);
            if (invoice == null)
            {
                return BadRequest();
            }

            return File(invoice, "application/pdf", $"Invoice-{invoiceId}.pdf");
        }
    }
}