using JjOnlineStore.Common.ViewModels;
using JjOnlineStore.Extensions;
using JjOnlineStore.Services.Core;
using JjOnlineStore.Services.Data;

using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;

using Optional;

using System;
using System.IO;
using System.Threading.Tasks;

using static JjOnlineStore.Services.Data.ServiceConstants;

namespace JjOnlineStore.Services.Business.Storage
{
    /// <summary>
    /// Stores images to Azure Storage service.
    /// </summary>
    public class ImageStorageService : IImageStorageService
    {
        /// <summary>
        /// Save image to storage if it doesn't exists.
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="image">Image content</param>
        /// <returns>Either file url to Azure or Error.</returns>
        public async Task<Option<string, Error>> StoreImageAsync(string filename, byte[] image)
        {
            //var filenameonly = Path.GetFileName(filename);

            //var url = string.Concat(BlobServiceStorageUrl, filenameonly);

            //var credentials = new StorageCredentials(BlobStorageAccount, BlobStorageKey);
            //var blob = new CloudBlockBlob(new Uri(url), credentials);

            //if (await blob.ExistsAsync())
            //{
            //    return Option.None<string, Error>("This image has already been submitted".ToError());
            //}

            //await blob.UploadFromByteArrayAsync(image, 0, image.Length);

            //return url.Some<string, Error>();

            // In Case of Local Testing: TODO: If Dev Env Check
            File.WriteAllBytes($@"D:\dms\{filename}", image);
            return $@"D:\dms\{filename}".Some<string, Error>();
        }
    }
}