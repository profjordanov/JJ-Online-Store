using System.Threading.Tasks;
using AutoMapper;
using JjOnlineStore.Data.EF;
using JjOnlineStore.Services.Business._Base;
using JjOnlineStore.Services.Core.Admin;
using Microsoft.EntityFrameworkCore;

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

        private async Task<bool> ExistsByName(string name)
            => await DbContext
                .Categories
                .AnyAsync(c => c.Name == name);
    }
}