using System.Linq;
using System.Threading.Tasks;
using JjOnlineStore.Data.EF;
using JjOnlineStore.Data.Entities;
using JjOnlineStore.Services.Business._Base;
using JjOnlineStore.Services.Core;
using Microsoft.EntityFrameworkCore;

using static JjOnlineStore.Services.Data.ServiceConstants;

namespace JjOnlineStore.Services.Business
{
    public class BillingService : BaseService, IBillingService
    {
        private readonly IPdfGenerator _pdfGenerator;
        public BillingService(JjOnlineStoreDbContext dbContext, IPdfGenerator pdfGenerator) 
            : base(dbContext)
        {
            _pdfGenerator = pdfGenerator;
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
            var invoiceData = await DbContext
                .Invoices
                .Where(i => i.Id == invoiceId)
                .Include(i => i.Order)
                .ThenInclude(o => o.OrderedItems)
                .ThenInclude(oi => oi.Product)
                .Select(i => new
                {
                    InvoiceNumber = i.Id,
                    InvoicePerson = $"{i.Order.FirstName} {i.Order.LastName}"
                })
                .FirstOrDefaultAsync();

            return _pdfGenerator.GeneratePdfFromHtml(string.Format(
                PdfInvoiceLayout,
                invoiceData.InvoiceNumber,
                invoiceData.InvoicePerson
            ));
        }
    }
}