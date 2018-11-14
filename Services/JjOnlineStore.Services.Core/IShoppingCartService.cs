using JjOnlineStore.Common.ViewModels.ShoppingCarts;
using JjOnlineStore.Services.Data.ShoppingCarts;

using System.Threading.Tasks;

namespace JjOnlineStore.Services.Core
{
    public interface IShoppingCartService
    {
        Task<CartVm> GetByUserIdAsync(string userId);
        Task CreateForUserByItsId(string userId);
        Task<ShoppingCartComponentServiceModel> GetCartComponentModelAsync(string userId);
    }
}
