using JjOnlineStore.Data.Entities.Base;

using System.Collections.Generic;

namespace JjOnlineStore.Data.Entities
{
    public class File : BaseDeletableModel<long>
    {
        public File(string path, string extension)
        {
            Path = path;
            Extension = extension;
        }

        public string Path { get; set; }

        public string Extension { get; set; }

        public virtual ICollection<ProductImage> ProductImages { get; set; } = new HashSet<ProductImage>();
    }
}