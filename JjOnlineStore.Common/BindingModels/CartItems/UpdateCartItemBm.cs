namespace JjOnlineStore.Common.BindingModels.CartItems
{
    public class UpdateCartItemBm
    {
        public string UserId { get; set; }

        public long CartId { get; set; }

        public CartItemShortBm[] CartItems { get; set; }
    }
}