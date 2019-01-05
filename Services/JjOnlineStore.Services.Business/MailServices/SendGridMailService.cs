using JjOnlineStore.Common.AppSettings.Sections;

using Microsoft.Extensions.Options;

using SendGrid;
using SendGrid.Helpers.Mail;

using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using JjOnlineStore.Services.Core;

namespace JjOnlineStore.Services.Business.MailServices
{
    /// <summary>
    /// 
    /// </summary>
    public class SendGridMailService : IMailService
    {
        private readonly AppMailSettings _mailSettings;

        public SendGridMailService(IOptions<AppMailSettings> mailSettings)
        {
            _mailSettings = mailSettings.Value;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="emailAddress"></param>
        /// <param name="subject"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public async Task<HttpStatusCode> SendEmailAsync(
            string emailAddress,
            string subject,
            string content)
        {
            var message = CreateAnEmailMessage(emailAddress, subject, content);
            var client = new SendGridClient(_mailSettings.SendGridApiKey);
            var response = await client.SendEmailAsync(message);
            return response.StatusCode;
        }

        private static SendGridMessage CreateAnEmailMessage(string emailAddress,
            string subject,
            string content)
        {
            var msg = new SendGridMessage();
            msg.SetFrom(new EmailAddress("jordanov.jordan@ue-varna.bg", "JJ Team"));
            var recipients = new List<EmailAddress>
            {
                new EmailAddress(emailAddress)
            };
            msg.AddTos(recipients);
            msg.SetSubject(subject);
            msg.AddContent(MimeType.Html, $"<p>{content}</p>");
            return msg;
        }
    }
}