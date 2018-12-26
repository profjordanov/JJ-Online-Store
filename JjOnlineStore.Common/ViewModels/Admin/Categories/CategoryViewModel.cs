using System.ComponentModel.DataAnnotations;
using JjOnlineStore.Common.Enumeration;

namespace JjOnlineStore.Common.ViewModels.Admin.Categories
{
    public class CategoryViewModel
    {
        public long Id { get; set; }

        [Required(ErrorMessage = "You must specify a category name.")]
        [MinLength(3, ErrorMessage = "Name must be at least 3 characters.")]
        [MaxLength(100, ErrorMessage = "Name can not be longer than 100 characters.")]
        [Display(Name = nameof(Name))]
        public string Name { get; set; }

        public MainStoreCategories StoreCategory { get; set; }
    }
}