using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace Codecool.CodecoolShop.Areas.Identity.Data
{
    public class EmailSender
    {
        public void SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var mail = "rysiekptysiek@o2.pl";
            var pw = "LubiePlacki3!";

            var client = new SmtpClient("poczta.o2.pl", 465)
            {
                EnableSsl = true,
                Credentials = new NetworkCredential(mail, pw)
            };

            client.Send(
                new MailMessage(from: mail,
                    to: email,
                    subject,
                    htmlMessage));
        }

    }
}
