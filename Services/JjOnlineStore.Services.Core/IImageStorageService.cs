using JjOnlineStore.Common.ViewModels;

using Optional;

using System.Threading.Tasks;


namespace JjOnlineStore.Services.Core
{
    public interface IImageStorageService
    {
        Task<Option<string, Error>> StoreImageAsync(string filename, byte[] image);
    }
}