using System.Collections.Generic;
using JjOnlineStore.Data.Entities.Base;

namespace JjOnlineStore.Data.Entities
{
    public class File : BaseDeletableModel<long>
    {
        public string Path { get; set; }

        public string Extension { get; set; }

        public virtual ICollection<ProductImage> ProductImages { get; set; } = new HashSet<ProductImage>();
    }
}