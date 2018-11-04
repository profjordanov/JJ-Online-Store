using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JjOnlineStore.Data.Entities
{
    public class OrderItem
    {
        [Key]
        public long Id { get; set; }

        public long ProductId { get; set; }

        public Product Product { get; set; }

        public short Quantity { get; set; }

        [ForeignKey(nameof(Order))]
        public long OrderId { get; set; }

        public Order Order { get; set; }

        public decimal TotalSum() =>
            Product.Price * Quantity;
    }
}