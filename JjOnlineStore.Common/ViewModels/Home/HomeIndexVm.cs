using JjOnlineStore.Common.ViewModels.Products;

using System.Collections.Generic;

namespace JjOnlineStore.Common.ViewModels.Home
{
    public class HomeIndexVm
    {
        public IEnumerable<ProductViewModel> NewItems { get; set; }
        public IEnumerable<ProductViewModel> OnSaleItems { get; set; }
        public IEnumerable<ProductViewModel> TopSellingItems { get; set; }
    }
}