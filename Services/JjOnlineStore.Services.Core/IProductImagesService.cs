using System.Collections.Generic;
using System.Threading.Tasks;
using JjOnlineStore.Data.Entities;

namespace JjOnlineStore.Services.Core
{
    public interface IProductImagesService
    {
        Task SaveImagesByProductAsync(Product product, IEnumerable<File> images);
    }
}