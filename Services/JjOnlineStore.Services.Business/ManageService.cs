using JjOnlineStore.Data.EF;
using JjOnlineStore.Services.Business._Base;
using JjOnlineStore.Services.Core;

namespace JjOnlineStore.Services.Business
{
    public class ManageService : BaseService, IManageService
    {
        public ManageService(JjOnlineStoreDbContext dbContext) 
            : base(dbContext)
        {
        }
    }
}