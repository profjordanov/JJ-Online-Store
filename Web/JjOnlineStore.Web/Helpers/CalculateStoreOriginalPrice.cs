namespace JjOnlineStore.Web.Helpers
{
    public static class CalculateStoreOriginalPrice
    {
        public static decimal ComputeValue(decimal price) =>
            price + price * 0.2m;
    }
}