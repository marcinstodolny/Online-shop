using System.ComponentModel.DataAnnotations;
using Domain;

namespace Codecool.CodecoolShop.Models
{
    public class Item :BaseModel
    {
        [Required]
        public Product Product { get; set; }
        [Required]
        public int Quantity { get; set; }
    }
}
