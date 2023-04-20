using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class Cart
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; } //to be used latter
        public List<Item>? Items { get; set; }
    }
}
