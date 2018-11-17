using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using JjOnlineStore.Common.Enumeration;
using JjOnlineStore.Data.Entities.Base;

namespace JjOnlineStore.Data.Entities
{
    public class Category : BaseDeletableModel<long>
    {
        [Required(ErrorMessage = "You must specify a category name.")]
        [MinLength(3, ErrorMessage = "Name must be at least 3 characters.")]
        [MaxLength(100, ErrorMessage = "Name can not be longer than 100 characters.")]
        public string Name { get; set; }

        public MainStoreCategories StoreCategory { get; set; }

        public virtual ICollection<Product> Products { get; set; } = new HashSet<Product>();
    }
}