using JjOnlineStore.Common.ViewModels.Products;
using System.Collections.Generic;

namespace JjOnlineStore.Common.ViewModels.Users
{
    public class UserProfileOverviewVm
    {
        public IEnumerable<ProductViewModel> Products { get; set; }
    }
}