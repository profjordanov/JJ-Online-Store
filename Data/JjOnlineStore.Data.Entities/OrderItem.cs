using System.ComponentModel.DataAnnotations;

namespace JjOnlineStore.Data.Entities
{
    public class OrderItem
    {
        [Key]
        public long Id { get; set; }

        public long ProductId { get; set; }

        public Product Product { get; set; }

        public short Quantity { get; set; }

        public long OrderId { get; set; }

        public Order Order { get; set; }
    }
}