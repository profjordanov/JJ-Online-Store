using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using JjOnlineStore.Data.Entities.Base;

namespace JjOnlineStore.Data.Entities
{
    public class Cart : BaseModel<long>
    {
        [ForeignKey(nameof(User))]
        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        public ICollection<CartItem> OrderedItems { get; set; } = new HashSet<CartItem>();
    }
}