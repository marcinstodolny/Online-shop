using Domain;

namespace Codecool.CodecoolShop.Models
{
    public class Cart : BaseModel
    {
        public List<Item> Items { get; set; }
    }
}
