using Domain;

namespace Codecool.CodecoolShop.Services
{
    public interface IEmailService
    {
        void SendEmailConfirmation(Order order, string total);
    }
}
