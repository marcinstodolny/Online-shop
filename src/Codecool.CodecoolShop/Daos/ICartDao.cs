using Codecool.CodecoolShop.Models;
using System.Collections.Generic;

namespace Codecool.CodecoolShop.Daos
{

    public interface ICartDao : IDao<Cart>
    {
        IEnumerable<Product> GetAllProductsBy(Cart cart);
    }
}