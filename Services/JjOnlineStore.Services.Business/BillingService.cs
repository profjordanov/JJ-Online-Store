using System.Threading.Tasks;
using JjOnlineStore.Data.EF;
using JjOnlineStore.Data.Entities;
using JjOnlineStore.Services.Business._Base;
using JjOnlineStore.Services.Core;

namespace JjOnlineStore.Services.Business
{
    public class BillingService : BaseService, IBillingService
    {
        public BillingService(JjOnlineStoreDbContext dbContext) 
            : base(dbContext)
        {
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
    }
}