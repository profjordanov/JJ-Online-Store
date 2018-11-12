using JjOnlineStore.Data.EF;
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
    }
}