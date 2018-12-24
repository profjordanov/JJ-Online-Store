using JjOnlineStore.Data.Entities.Base;

namespace JjOnlineStore.Data.Entities
{
    public class ProductImage : BaseDeletableModel<long>
    {
        public ProductImage(long fileId, long productId)
        {
            FileId = fileId;
            ProductId = productId;
        }

        public long FileId { get; set; }
        public File File { get; set; }

        public long ProductId { get; set; }
        public Product Product { get; set; }
    }
}