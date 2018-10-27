using JjOnlineStore.Common.ViewModels.Products;

namespace JjOnlineStore.Common.ViewModels.CartItems
{
    public class CartItemVm
    {
        public long Id { get; set; }

        public long ProductId { get; set; }

        public ProductViewModel Product { get; set; }

        public short Quantity { get; set; }

        public long CartId { get; set; }
    }
}