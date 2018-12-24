using JjOnlineStore.Data.EF;
using JjOnlineStore.Services.Business._Base;
using JjOnlineStore.Services.Core;

namespace JjOnlineStore.Services.Business
{
    public class FileService : BaseService, IFileService
    {
        public FileService(JjOnlineStoreDbContext dbContext) 
            : base(dbContext)
        {
        }
    }
}