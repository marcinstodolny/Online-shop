using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class Cart
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string UserId { get; set; }
        [Required]
        public string ItemsJson { get; set; }
    }
}
