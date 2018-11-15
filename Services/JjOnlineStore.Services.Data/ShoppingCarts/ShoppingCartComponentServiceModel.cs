using System.Collections.Generic;
using System.Linq;
using JjOnlineStore.Common.ViewModels.CartItems;

namespace JjOnlineStore.Services.Data.ShoppingCarts
{
    public class ShoppingCartComponentServiceModel
    {
        public IEnumerable<CartItemVm> CartItems { get; set; }

        public int GetItemsCount() =>
            CartItems.Count();

        public decimal GetProductsGrandTotal() =>
            CartItems.Sum(ci => ci.TotalSum());
    }
}