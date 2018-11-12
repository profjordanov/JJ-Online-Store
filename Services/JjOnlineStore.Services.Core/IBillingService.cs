using System.Threading.Tasks;

namespace JjOnlineStore.Services.Core
{
    public interface IBillingService
    {
        Task CreateInvoiceByOrderIdAsync(long orderId);
    }
}