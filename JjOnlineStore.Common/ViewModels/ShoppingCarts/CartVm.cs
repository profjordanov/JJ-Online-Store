using System.Collections.Generic;
using System.Linq;
using JjOnlineStore.Common.ViewModels.CartItems;

namespace JjOnlineStore.Common.ViewModels.ShoppingCarts
{
    public class CartVm
    {
        public long Id { get; set; }

        public string UserId { get; set; }

        public IEnumerable<CartItemVm> CartItems { get; set; }

        public decimal GrandTotal() =>
            CartItems.Sum(ci => ci.TotalSum());
    }
}