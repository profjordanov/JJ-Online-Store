namespace JjOnlineStore.Services.Data
{
    public static class ServiceConstants
    {
        public const string BlobServiceStorageUrl = "";
        public const string BlobStorageAccount = "";
        public const string BlobStorageKey = "";

        public const string CartItemExistsErrMsg = "The product has already been added to the cart.";
        public const string OrderConfirmationErrMsg = "Something happened and we " +
                                     "couldn't process confirmation of the order! Try again later!";
        public const string CartItemsDeleteErrMsg = "Opps..Some of your products " +
                                                    "are still in the shopping cart!";

        //HTML
        public const string PdfInvoiceLayout = @"
        <h1>Invoice #: {0}</h1>
        <br />
        InvoicePerson {1}
        {2} {3}
        ";
    }
}