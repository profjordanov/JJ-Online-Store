using JjOnlineStore.Data.Entities.Base;

namespace JjOnlineStore.Data.Entities
{
    public class Invoice : BaseModel<long>
    {
        public long OrderId { get; set; }
        public Order Order { get; set; }
    }
}