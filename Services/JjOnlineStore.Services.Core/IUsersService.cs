using System.Threading.Tasks;
using JjOnlineStore.Common.ViewModels;
using JjOnlineStore.Services.Data;
using Optional;

namespace JjOnlineStore.Services.Core
{
    public interface IUsersService
    {
        Task<Option<RegisterServiceModel, Error>> Register(RegisterViewModel model);
    }
}