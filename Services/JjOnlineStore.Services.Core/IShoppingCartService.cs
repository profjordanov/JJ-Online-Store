using Optional;
using System.Threading.Tasks;
using JjOnlineStore.Common.ViewModels;
using JjOnlineStore.Common.ViewModels.ShoppingCarts;
using JjOnlineStore.Data.Entities;

namespace JjOnlineStore.Services.Core
{
    public interface IShoppingCartService
    {
        Task<CartVm> GetById(long shoppingCartId);
        Task<Option<long, Error>> CreateByUsernameAsync(string username);
        Task<long> CreateByUserAsync(ApplicationUser user);
        Task CreateForUserByItsId(string userId);
    }
}
