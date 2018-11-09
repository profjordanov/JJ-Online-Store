using JjOnlineStore.Data.EF;
using JjOnlineStore.Services.Business._Base;
using JjOnlineStore.Services.Core;
using JjOnlineStore.Common.ViewModels;
using JjOnlineStore.Data.Entities;
using JjOnlineStore.Extensions;

using AutoMapper;

using Optional;

using Serilog;

using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using static JjOnlineStore.Services.Data.ServiceConstants;

namespace JjOnlineStore.Services.Business
{
    public class OrderItemsService : BaseService, IOrderItemsService
    {
        private readonly ILogger _log;
        public OrderItemsService(JjOnlineStoreDbContext dbContext, IMapper mapper, ILogger log) 
            : base(dbContext)
        {
            Mapper = mapper;
            _log = log;
        }

        protected IMapper Mapper { get; }

        public async Task<Option<IEnumerable<OrderItem>,Error>> CreateRangeByUserIdAndOrderIdAsync(
            string userId,
            long orderId)
        {
            var shoppingCart = await DbContext.Carts
                .Include(sc => sc.OrderedItems)
                .ThenInclude(oi => oi.Product)
                .Where(sc => sc.UserId == userId)
                .FirstOrDefaultAsync();

            var orderItems =
                Mapper.Map<IEnumerable<CartItem>, 
                        IEnumerable<OrderItem>>(shoppingCart.OrderedItems)
                    .ToList();
            orderItems.ForEach(oi => oi.OrderId = orderId);

            try
            {
                DbContext.BeginTransaction();
                await DbContext.OrderItems.AddRangeAsync(orderItems);
                await DbContext.CommitTransactionAsync();
            }
            catch (Exception ex)
            {
                _log.Error(ex, ex.Message);
                return Option.None<IEnumerable<OrderItem>, Error>(OrderConfirmationErrMsg.ToError());
            }

            return orderItems.Some<IEnumerable<OrderItem>, Error>();
        }
    }
}