using System.Collections.Generic;
using Domain;
using Product = Codecool.CodecoolShop.Models.Product;

namespace Codecool.CodecoolShop.Daos
{

    public interface ICartDao : IDao<Cart>
    {
        IEnumerable<Product> GetAllProductsBy(Cart cart);
    }
}