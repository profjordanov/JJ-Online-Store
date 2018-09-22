using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using JjOnlineStore.Data.Entities.Base;
using JjOnlineStore.Data.Entities.Enumeration;

namespace JjOnlineStore.Data.Entities
{
    public class Product : BaseDeletableModel<long>
    {
        [Required(ErrorMessage = "Please enter a name.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter a description.")]
        public string Description { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Please, enter a positive price.")]
        public decimal Price { get; set; }

        [Required]
        public string Base64Image { get; set; }

        public bool IsAvailable { get; set; }

        public Size Size { get; set; }

        public string Color { get; set; }

        public ProductType Type { get; set; }

        public string Details { get; set; }

        public long CategoryId { get; set; }

        [Required(ErrorMessage = "Please, specify a category.")]
        public virtual Category Category { get; set; }
    }
}