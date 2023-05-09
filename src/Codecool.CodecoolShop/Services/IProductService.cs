using System.Collections.Generic;
using Domain;

namespace Codecool.CodecoolShop.Services
{
    public interface IProductService
    {
        List<Product> GetAllProducts();
        List<ProductCategory> GetProductCategories();
        List<Supplier> GetSuppliers();
        List<Product> GetProductsByCategory(int categoryId);
        List<Product> GetProductsBySupplier(int supplierId);
    }
}
