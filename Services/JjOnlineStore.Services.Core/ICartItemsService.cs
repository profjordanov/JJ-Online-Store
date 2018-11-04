using Optional;
using System.Threading.Tasks;
using JjOnlineStore.Common.BindingModels.CartItems;
using JjOnlineStore.Services.Data.CartItems;
using JjOnlineStore.Common.ViewModels;

namespace JjOnlineStore.Services.Core
{
    public interface ICartItemsService
    {
        Task<Option<CartItemServiceModel, Error>> CreateAsync(CartItemBm cartItem);
    }
}
