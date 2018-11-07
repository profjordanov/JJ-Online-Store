using JjOnlineStore.Common.ViewModels.ShoppingCarts;

using System.Threading.Tasks;


namespace JjOnlineStore.Services.Core
{
    public interface IShoppingCartService
    {
        Task<CartVm> GetByUserIdAsync(string userId);
        Task CreateForUserByItsId(string userId);
    }
}
