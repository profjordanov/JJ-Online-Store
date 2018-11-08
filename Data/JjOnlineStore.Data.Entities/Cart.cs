using JjOnlineStore.Data.Entities.Base;

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace JjOnlineStore.Data.Entities
{
    public class Cart : BaseModel<long>
    {
        [ForeignKey(nameof(User))]
        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        public ICollection<CartItem> OrderedItems { get; set; } = new HashSet<CartItem>();

        public decimal GrandTotal =>
            OrderedItems.Sum(oi => oi.TotalSum());
    }
}