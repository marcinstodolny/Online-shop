using System.Collections.Generic;
using System.Linq;
using Codecool.CodecoolShop.Models;
using Domain;

namespace Codecool.CodecoolShop.Daos.Implementations
{
    public class CartDaoMemory
    {
        private List<Cart> data = new List<Cart>();

        private CartDaoMemory()
        {
        }

        public void Add(Cart item)
        {
            item.Id = data.Count + 1;
            data.Add(item);
        }

        public void Remove(int id)
        {
            data.Remove(this.Get(id));
        }

        public Cart Get(int id)
        {
            return data.Find(x => x.Id == id);
        }

        public IEnumerable<Cart> GetAll()
        {
            return data;
        }
        //public IEnumerable<Domain.Product> GetAllItemsBy(Cart cart)
        //{
            
        //    return data.Where(item => item.Id == cart.Id).SelectMany(item => item.Items).Select(item => item.Product);
        //}

        //public IEnumerable<Cart> GetBy(Supplier supplier)
        //{
        //    return data.Where(x => x.Supplier.Id == supplier.Id);
        //}

        //public IEnumerable<Cart> GetBy(ProductCategory productCategory)
        //{
        //    return data.Where(x => x.ProductCategory.Id == productCategory.Id);
        //}
    }
}
