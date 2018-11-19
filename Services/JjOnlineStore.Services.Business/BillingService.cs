using System.Globalization;
using System.Linq;
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

            var invoiceData = await DbContext
                .Invoices
                .Where(i => i.Id == invoiceId)
                .Include(i => i.Order)
                .ThenInclude(o => o.OrderedItems)
                .ThenInclude(oi => oi.Product)
                .Select(i => new
                {
                    InvoiceNumber = i.Id,
                    DateCreated = i.CreatedOn.ToString(CultureInfo.InvariantCulture),
                    InvoicePerson = $"{i.Order.FirstName} {i.Order.LastName}",
                    InvoicePersonAddress = $"{i.Order.Country}, {i.Order.City}, {i.Order.Address}",
                    OrderedItemsWithoutLast = i.Order.OrderedItems.SkipLast(1),
                    LastOrderedItem = i.Order.OrderedItems.LastOrDefault()
                })
                .FirstOrDefaultAsync();

            var x = _pdfGenerator.GeneratePdfFromHtml(string.Format(
                invoiceLayout,
                invoiceData.InvoiceNumber,
                invoiceData.DateCreated,
                invoiceData.InvoicePerson,
                invoiceData.InvoicePersonAddress
            ));

            return x;
        }
    }
}