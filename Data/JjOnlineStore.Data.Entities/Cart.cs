using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using JjOnlineStore.Data.Entities.Base;

namespace JjOnlineStore.Data.Entities
{
    public class Cart : BaseModel<long>
    {
        [ForeignKey("User")]
        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        public virtual ICollection<CartItem> OrderedItems { get; set; } = new HashSet<CartItem>();

        public decimal TotalSum()
            => this.OrderedItems.Sum(oi => oi.Product.Price * oi.Quantity);
    }
}