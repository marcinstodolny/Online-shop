using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class Product : BaseModel
    {
        [Required]
        public string Currency { get; set; }
        [Required]
        public decimal DefaultPrice { get; set; }
        public ProductCategory ProductCategory { get; set; }
        public Supplier Supplier { get; set; }

        public void SetProductCategory(ProductCategory productCategory)
        {
            ProductCategory = productCategory;
            //ProductCategory.Products.Add(this);
        }
    }
}
