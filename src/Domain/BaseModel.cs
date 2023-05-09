using System.ComponentModel.DataAnnotations;


namespace Domain
{
    public abstract class BaseModel
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }
    }
}
