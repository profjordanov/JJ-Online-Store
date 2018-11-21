using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JjOnlineStore.Data.EF;
using JjOnlineStore.Data.Entities;
using JjOnlineStore.Services.Business._Base;
using JjOnlineStore.Services.Core;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;

using static JjOnlineStore.Services.Data.ServiceConstants;
using static System.IO.Path;
using static System.IO.File;

namespace JjOnlineStore.Services.Business
{
    public class BillingService : BaseService, IBillingService
    {
        private readonly IPdfGenerator _pdfGenerator;
        private readonly IHostingEnvironment _env;

        public BillingService(
            JjOnlineStoreDbContext dbContext,
            IPdfGenerator pdfGenerator,
            IHostingEnvironment env) 
            : base(dbContext)
        {
            _pdfGenerator = pdfGenerator;
            _env = env;
        }

        public async Task CreateInvoiceByOrderIdAsync(long orderId)
        {
            var entity = new Invoice
            {
                OrderId = orderId
            };

            await DbContext.Invoices.AddAsync(entity);
            await DbContext.SaveChangesAsync();

            var order = await DbContext.Orders.FindAsync(orderId);
            order.InvoiceId = entity.Id;
            DbContext.Orders.Update(order);
            await DbContext.SaveChangesAsync();
        }

        public async Task<byte[]> GetPdfInvoiceAsync(long invoiceId)
        {
            var invoiceHtmPath = _env.WebRootPath +
                                 DirectorySeparatorChar +
                                 "invoice" +
                                 DirectorySeparatorChar +
                                 "invoice.html";

            var invoiceLayout = ReadAllText(invoiceHtmPath);

            var invoice = await DbContext
                .Invoices
                .Include(i => i.Order)
                .Where(i => i.Id == invoiceId)
                .FirstOrDefaultAsync();

            invoice.Order.OrderedItems = await DbContext
                .OrderItems
                .Include(oi => oi.Product)
                .Where(oi => oi.OrderId == invoice.OrderId)
                .ToListAsync();

            var invoiceData = new
            {
                InvoiceNumber = invoice.Id,
                DateCreated = invoice.CreatedOn.ToString(CultureInfo.InvariantCulture),
                InvoicePerson = 
                    $"{invoice.Order.FirstName} {invoice.Order.LastName}",
                InvoicePersonAddress = 
                    $"{invoice.Order.Country}, {invoice.Order.City}, {invoice.Order.Address}",
                OrderedItems = invoice.Order.OrderedItems,
                GrandTotal = invoice.Order.OrderedItems.Sum(oi => oi.TotalSum())
            };

            return _pdfGenerator.GeneratePdfFromHtml(string.Format(
                invoiceLayout,
                invoiceData.InvoiceNumber,
                invoiceData.DateCreated,
                invoiceData.InvoicePerson,
                invoiceData.InvoicePersonAddress,
                StringifiedOrderedItemsForHtmlTable(invoiceData.OrderedItems),
                invoiceData.GrandTotal
            ));
        }

        private static string StringifiedOrderedItemsForHtmlTable(IEnumerable<OrderItem> orderItems)
        {
            var sb = new StringBuilder();
            foreach (var orderItem in orderItems)
            {
                sb.Append("<tr>");
                sb.Append("<td>");
                sb.Append(orderItem.Product.Name);
                sb.Append("</td>");
                sb.Append("<td>");
                sb.Append(orderItem
                    .TotalSum()
                    .ToString(CultureInfo.InvariantCulture));
                sb.Append("</td>");
                sb.Append("</tr>");
            }

            return sb.ToString();
        }
    }
}