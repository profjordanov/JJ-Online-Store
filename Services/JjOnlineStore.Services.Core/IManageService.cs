using JjOnlineStore.Common.ViewModels;
using JjOnlineStore.Common.ViewModels.Manage;
using JjOnlineStore.Services.Data.Users;

using Optional;

using System.Threading.Tasks;

namespace JjOnlineStore.Services.Core
{
    public interface IManageService
    {
        Task<Option<UserServiceModel, Error>> ChangePasswordAsync(ChangePasswordVm model);
    }
}