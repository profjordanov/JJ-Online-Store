using JjOnlineStore.Data.EF;

namespace JjOnlineStore.Services.Business._Base
{
    public abstract class BaseService
    {
        protected BaseService(JjOnlineStoreDbContext dbContext)
        {
            DbContext = dbContext;
        }

        protected JjOnlineStoreDbContext DbContext { get; }
    }
}