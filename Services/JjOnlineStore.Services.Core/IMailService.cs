using System.Net;
using System.Threading.Tasks;
using SendGrid.Helpers.Mail;

namespace JjOnlineStore.Services.Core
{
    public interface IMailService
    {
        Task<HttpStatusCode> SendEmailAsync(
            string emailAddress,
            string subject,
            string content);
    }
}