using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Net.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

using eMedicEntityModel.Models.v1;

namespace eMedicNETv7.Services
{
    public class EmailSender : IEmailSender
    {
        private readonly IOptions<SMTPSettings> _smtpSettings;

        public EmailSender(IOptions<SMTPSettings> smtpSettings)
        {
            _smtpSettings = smtpSettings;
        }
        public Task SendEmailAsync(string email, string subject, string message)
        {
            MailMessage mailMsg = new MailMessage();

            mailMsg.From = new MailAddress("no-reply@valuecreatingsolutions.com", "VCS");
            mailMsg.To.Add(new MailAddress(email));

            mailMsg.Subject = subject;
            mailMsg.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(message, null, MediaTypeNames.Text.Plain));
            mailMsg.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(message, null, MediaTypeNames.Text.Html));

            SmtpClient smtpClient = new SmtpClient("smtp.zoho.com", 587);
            smtpClient.EnableSsl = true;
            smtpClient.Timeout = 0;
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpClient.UseDefaultCredentials = true;
            smtpClient.Credentials = new NetworkCredential("no-reply@valuecreatingsolutions.com", "Vijay$0316");

            smtpClient.Send(mailMsg);
            return Task.CompletedTask;
        }
    }
}
