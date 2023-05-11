using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class Item
    {
        [Key]
        public int Id { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }
}
