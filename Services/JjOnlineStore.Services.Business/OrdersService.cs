using AutoMapper;
using JjOnlineStore.Data.EF;
using JjOnlineStore.Services.Business._Base;
using JjOnlineStore.Services.Core;

namespace JjOnlineStore.Services.Business
{
    public class OrdersService : BaseService, IOrdersService
    {
        public OrdersService(JjOnlineStoreDbContext dbContext, IMapper mapper) 
            : base(dbContext)
        {
            Mapper = mapper;
        }

        protected IMapper Mapper { get; }
    }
}