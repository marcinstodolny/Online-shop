using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class Item
    {
        [Required]
        public Product Product { get; set; }
        [Required]
        public int Quantity { get; set; }
    }
}
