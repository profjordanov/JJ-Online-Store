namespace JjOnlineStore.Services.Core
{
    public interface IPdfGenerator
    {
        byte[] GeneratePdfFromHtml(string html);
    }
}