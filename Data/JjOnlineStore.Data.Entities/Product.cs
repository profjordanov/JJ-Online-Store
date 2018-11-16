using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using JjOnlineStore.Common.Enumeration;
using JjOnlineStore.Data.Entities.Base;

namespace JjOnlineStore.Data.Entities
{
    public class Product : BaseDeletableModel<long>
    {
        [Required(ErrorMessage = "Please enter a name.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter a description.")]
        public string Description { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public string Base64Image { get; set; }

        public bool IsAvailable { get; set; }

        public Size Size { get; set; }

        public string Color { get; set; }

        public ProductType Type { get; set; }

        public string Details { get; set; }

        [ForeignKey(nameof(Category))]
        public long CategoryId { get; set; }

        [Required(ErrorMessage = "Please, specify a category.")]
        public virtual Category Category { get; set; }

        public virtual ICollection<CartItem> CartItems { get; set; } = new HashSet<CartItem>();

        public virtual ICollection<OrderItem> OrderItems { get; set; } = new HashSet<OrderItem>();

        public virtual ICollection<UserViewedItem> UserViewed { get; set; } = new HashSet<UserViewedItem>();
    }
}