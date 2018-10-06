using AutoMapper;
using JjOnlineStore.Data.EF;
using JjOnlineStore.Services.Business._Base;
using JjOnlineStore.Services.Core.Admin;

namespace JjOnlineStore.Services.Business.Admin
{
    public class AdminCategoryService : BaseService, IAdminCategoryService
    {
        public AdminCategoryService(JjOnlineStoreDbContext dbContext, IMapper mapper) 
            : base(dbContext)
        {
            Mapper = mapper;
        }

        protected IMapper Mapper { get; }
    }
}