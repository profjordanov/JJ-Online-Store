using System.Collections.Generic;
using JjOnlineStore.Common.ViewModels.CartItems;

namespace JjOnlineStore.Common.ViewModels.ShoppingCarts
{
    public class CartVm
    {
        public long Id { get; set; }

        public IEnumerable<CartItemVm> CartItems { get; set; }

        public decimal TotalSum { get; }
    }
}