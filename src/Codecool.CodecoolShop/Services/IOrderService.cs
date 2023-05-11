using Domain;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;

namespace Codecool.CodecoolShop.Services
{
    public interface IOrderService
    {
        void AddOrder(Order order);
        List<Domain.Order> GetAllOrders();
        void SaveOrderToJson(Order order);
        void SendEmailConfirmation(Order order, string total);
    }
}