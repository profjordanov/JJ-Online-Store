using System.ComponentModel.DataAnnotations;
using JjOnlineStore.Common.Enumeration;
using JjOnlineStore.Common.ViewModels.Admin.Categories;

namespace JjOnlineStore.Common.ViewModels.Products
{
    public class ProductViewModel
    {
        public long Id { get; set; }

        [Required(ErrorMessage = "Please enter a name.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter a description.")]
        public string Description { get; set; }

        public string ShortDescription
            => $"{Description.Substring(0, 20)}...";

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
        public virtual CategoryViewModel Category { get; set; }
    }
}