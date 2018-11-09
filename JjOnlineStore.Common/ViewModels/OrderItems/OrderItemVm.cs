using JjOnlineStore.Common.ViewModels.Orders;
using JjOnlineStore.Common.ViewModels.Products;

namespace JjOnlineStore.Common.ViewModels.OrderItems
{
    public class OrderItemVm
    {
        public long Id { get; set; }

        public long ProductId { get; set; }

        public ProductViewModel Product { get; set; }

        public short Quantity { get; set; }

        public long OrderId { get; set; }

        public OrderVm Order { get; set; }

        public decimal TotalSum() =>
            Product.Price * Quantity;
    }
}