using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    public class Item
    {
        public Product Product { get; set; }

        public int Quantity { get; set; }
    }
}
