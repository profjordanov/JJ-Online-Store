using JjOnlineStore.Common.BindingModels.CartItems;
using JjOnlineStore.Services.Data.CartItems;
using JjOnlineStore.Common.ViewModels;

using Optional;

using System.Threading.Tasks;


namespace JjOnlineStore.Services.Core
{
    public interface ICartItemsService
    {
        Task<Option<CartItemServiceModel, Error>> CreateAsync(CartItemBm cartItem);
        Task SetDeletedByIdAsync(long cartItemId);
        Task<UpdateCartItemBm> UpdateAsync(UpdateCartItemBm model);
        Task DeleteAllInCartByUserIdAsync(string userId);
    }
}
