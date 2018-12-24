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
        /// Adds new records to `ProductImages` database table.
        /// </summary>
        /// <param name="product">Current product.</param>
        /// <param name="images">Image paths.</param>
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