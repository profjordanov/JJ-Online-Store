using System;
using JjOnlineStore.Common.BindingModels.Emails;
using JjOnlineStore.Services.Core;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using System.Threading.Tasks;

namespace JjOnlineStore.Web.Controllers
{
    [AllowAnonymous]
    [Route("[controller]/[action]")]
    public class SubscriptionController : BaseController
    {
        private readonly IMailService _mailService;

        public SubscriptionController(IMailService mailService)
        {
            _mailService = mailService;
        }

        /// POST: /Subscription/Subscribe
        /// <summary>
        /// Sends a subscription email.
        /// </summary>
        /// <param name="model"></param>
        /// <response code="200"></response>
        [HttpPost]
        [Produces("application/json")]
        public async Task<IActionResult> Subscribe([FromBody] EmailBm model)
        {
            await _mailService.SendEmailAsync(
                emailAddress: model.Email,
                subject: "Welcome to my store!",
                content: "Do you want spam! Its not a problem for me, but it is a problem for you!");

            return Ok(new object()); 
        }
    }
}