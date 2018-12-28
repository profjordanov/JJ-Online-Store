namespace JjOnlineStore.Common.BindingModels.Files
{
    public class HttpFile
    {
        public string FileName { get; set; }

        public string ContentType { get; set; }

        public byte[] Content { get; set; }
    }
}