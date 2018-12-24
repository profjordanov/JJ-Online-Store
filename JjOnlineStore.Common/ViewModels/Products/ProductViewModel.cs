using JjOnlineStore.Common.Enumeration;
using JjOnlineStore.Common.ViewModels.Admin.Categories;

using Microsoft.AspNetCore.Http;

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace JjOnlineStore.Common.ViewModels.Products
{
    public class ProductViewModel
    {
        public long Id { get; set; }

        [Required(ErrorMessage = "Please enter a name.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter a description.")]
        [MinLength(20)]
        public string Description { get; set; }

        public string ShortDescription
            => $"{Description.Substring(0, 20)}...";

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Please, enter a positive price.")]
        public decimal Price { get; set; }

        public string Base64Image { get; set; }

        public IList<IFormFile> FormImages { get; set; }

        public bool IsAvailable { get; set; }

        public Size Size { get; set; }

        public string Color { get; set; }

        public ProductType Type { get; set; }

        public string Details { get; set; }

        [Display(Name = "Category")]
        public long CategoryId { get; set; }

        [Required(ErrorMessage = "Please, specify a category.")]
        public virtual CategoryViewModel Category { get; set; }
    }
}