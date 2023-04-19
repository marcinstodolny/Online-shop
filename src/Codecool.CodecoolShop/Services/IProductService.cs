using System.Collections.Generic;
using Data;
using Domain;

namespace Codecool.CodecoolShop.Services
{
    public interface IProductService
    {
        List<Product> GetAllProducts();
        List<ProductCategory> GetProductCategories();
        List<Supplier> GetSuppliers();
    }
}
