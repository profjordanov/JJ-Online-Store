using System.Threading.Tasks;
using JjOnlineStore.Common.ViewModels;
using Optional;

namespace JjOnlineStore.Services.Core
{
    public interface IImageStorageService
    {
        Task<Option<string, Error>> StoreImage(string filename, byte[] image);
    }
}