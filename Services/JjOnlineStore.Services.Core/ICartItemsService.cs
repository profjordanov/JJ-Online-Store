using System.Threading.Tasks;
using JjOnlineStore.Common.BindingModels.CartItems;
using JjOnlineStore.Services.Data.CartItems;

namespace JjOnlineStore.Services.Core
{
    public interface ICartItemsService
    {
        Task<CartItemServiceModel> CreateAsync(CartItemBm cartItem);
    }
}
