using JjOnlineStore.Common.ViewModels;
using JjOnlineStore.Common.BindingModels.Files;

using Optional;

using System.Threading.Tasks;


namespace JjOnlineStore.Services.Core
{
    public interface IImageStorageService
    {
        Task<Option<string, Error>> StoreImageAsync(string filename, byte[] image);
        Task<Option<HttpFile, Error>> GetImageAsync(string filepath);
    }
}