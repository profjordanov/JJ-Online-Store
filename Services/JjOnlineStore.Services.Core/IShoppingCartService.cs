using Optional;
using System.Threading.Tasks;
using JjOnlineStore.Common.ViewModels;
using JjOnlineStore.Data.Entities;

namespace JjOnlineStore.Services.Core
{
    public interface IShoppingCartService
    {
        Task<Option<long, Error>> CreateByUsernameAsync(string username);
        Task<long> CreateByUserAsync(ApplicationUser user);
    }
}
