using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace Codecool.CodecoolShop.Areas.Identity.Data
{
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var mail = "RysiekPtysiek2@outlook.com";
            var pw = "LubiePlacki3!";

            var client = new SmtpClient("smtp-mail.outlook.com", 587)
            {
                EnableSsl = true,
                Credentials = new NetworkCredential(mail, pw)
            };
            var msg = new MailMessage(from: mail, to: email, subject, htmlMessage);
            msg.IsBodyHtml = true;
            return client.SendMailAsync(msg);
        }

    }
}
