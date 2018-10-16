using System.Threading.Tasks;
using JjOnlineStore.Common.ViewModels;
using JjOnlineStore.Common.ViewModels.Account;
using JjOnlineStore.Services.Data;
using JjOnlineStore.Services.Data.Users;
using Optional;

namespace JjOnlineStore.Services.Core
{
    public interface IUsersService
    {
        Task<Option<UserServiceModel, Error>> LoginAsync(CredentialsModel model);
        Task<Option<UserServiceModel, Error>> Register(RegisterViewModel model);
    }
}