using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Encodings.Web;

using eMedicNETv6.Services;

namespace eMedicNETv6.Extensions
{
    public static class EmailSenderExtensions
    {
        public static Task SendEmailConfirmationAsync(this IEmailSender emailSender, string email, string link)
        {
            string msg = $"Welcome to eMedic.NET Version 5.0<br/><br/>Please click <a href='{HtmlEncoder.Default.Encode(link)}'>here</a> to activate your user account.<br/><br/>If you have any queries please do not hesiate to contact us on support@valuecreatingsolutions.com<br/><br/>For emergency support you can contact Mr. Raj +6019 337 9720<br/>Supporting Engineer<br/>Value Creating Solutions";
            return emailSender.SendEmailAsync(email, "Confirm your email", msg);
        }
    }
}
