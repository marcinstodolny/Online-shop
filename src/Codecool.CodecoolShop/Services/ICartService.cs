using System;
using System.Collections.Generic;
using Data;
using Domain;

namespace Codecool.CodecoolShop.Services
{
    public interface ICartService
    {
        List<Product> GetAllProducts();
        List<ProductCategory> GetProductCategories();
        List<Supplier> GetSuppliers();
        List<Product> GetProductsByCategory(int categoryId);
        List<Product> GetProductsBySupplier(int supplierId);
        Product FindProductById(string id);
        bool SaveCartToDb(string UserId, string items);
    }
}