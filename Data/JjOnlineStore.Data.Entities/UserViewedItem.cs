using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JjOnlineStore.Data.Entities
{
    public class UserViewedItem
    {
        [Key]
        public long Id { get; set; }

        [ForeignKey(nameof(User))]
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        [ForeignKey(nameof(Product))]
        public long ProductId { get; set; }
        public Product Product { get; set; }
    }
}