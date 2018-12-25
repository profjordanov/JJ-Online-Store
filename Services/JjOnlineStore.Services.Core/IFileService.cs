using System.Collections.Generic;
using System.Threading.Tasks;
using JjOnlineStore.Data.Entities;
using Microsoft.AspNetCore.Http;

namespace JjOnlineStore.Services.Core
{
    public interface IFileService
    {
        Task<IEnumerable<File>> SaveImageFilesAsync(IEnumerable<IFormFile> files);
        Task<string[]> StoreImagesAsync(IEnumerable<IFormFile> files);
    }
}