using Data;
using Domain;
using Microsoft.Extensions.Options;
using System.Net.Mail;
using System.Net;

namespace Codecool.CodecoolShop.Services
{
    public class EmailService : IEmailService
    {
        private readonly EmailContext _emailContext;

        public EmailService(IOptions<EmailContext> emailContext)
        {
            _emailContext = emailContext.Value;
        }

        public void SendEmailConfirmation(Order order, string total)
        {
            var client = new SmtpClient(_emailContext.Smtp, _emailContext.Port)
            {
                Credentials = new NetworkCredential(_emailContext.Username, _emailContext.Password),
                EnableSsl = true
            };

            var message = new MailMessage();

            message.From = new MailAddress(_emailContext.Address);
            message.To.Add(new MailAddress(order.Email));

            message.Subject = "Order Confirmation";
            message.Body = $"Dear {order.Name},\n\nThank you for your order. Your order has been confirmed.\n\nShipping Address:\n{order.ShippingAddress}\n\nTotal amount: {total}\n\nBest Regards,\nYour Online Shop";

            client.Send(message);
        }
    }
}
