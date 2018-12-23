using JjOnlineStore.Data.Entities.Base;

namespace JjOnlineStore.Data.Entities
{
    /// <summary>
    /// This table will be filled when a Cart Item is deleted
    /// via `TR_CartItems_DLT` trigger.
    /// </summary>
    /// <see>
    ///     <cref>database-scripts.sql</cref>
    /// </see>
    public class CartItemOnDeleteReport : BaseModel<int>
    {
        public long ProductId { get; set; }

        public Product Product { get; set; }

        public short Quantity { get; set; }

        public long CartId { get; set; }

        public Cart Cart { get; set; }
    }
}