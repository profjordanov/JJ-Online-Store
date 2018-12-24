using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JjOnlineStore.Data.EF;
using JjOnlineStore.Data.Entities;
using JjOnlineStore.Services.Business._Base;
using JjOnlineStore.Services.Core;

namespace JjOnlineStore.Services.Business
{
    public class ProductImagesService : BaseService, IProductImagesService
    {
        public ProductImagesService(JjOnlineStoreDbContext dbContext) 
            : base(dbContext)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="product"></param>
        /// <param name="images"></param>
        /// <returns></returns>
        public async Task SaveImagesByProductAsync(
            Product product,
            IEnumerable<File> images)
        {
            var productImages = images
                .Select(imgFile => new ProductImage(
                    fileId: imgFile.Id,
                    productId: product.Id));

            await DbContext.ProductImages.AddRangeAsync(productImages);
            await DbContext.SaveChangesAsync();
        }
    }
}