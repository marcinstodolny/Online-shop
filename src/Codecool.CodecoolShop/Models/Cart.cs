using System.Collections.Generic;

namespace Codecool.CodecoolShop.Models
{
    public class Cart : BaseModel
    {
        public List<Product> Products { get; set; }
    }
}
