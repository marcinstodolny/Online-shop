using Codecool.CodecoolShop.Validators;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Order : BaseModel
    {
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Phone is required")]
        [Phone(ErrorMessage = "invalid phone format")]
        public string Phone { get; set; }

        [DisplayName("Billing Country")]
        [Required(ErrorMessage = "Billing country is required")]
        public string BillingCountry { get; set; }

        [DisplayName("Billing City")]
        [Required(ErrorMessage = "Billing City is required")]
        public string BillingCity { get; set; }

        [DisplayName("Billing Zip-Code")]
        [Required(ErrorMessage = "Billing Zip-Code is required")]
        public string BillingZipcode { get; set; }

        [DisplayName("Billing Address")]
        [Required(ErrorMessage = "Billing Address is required")]
        public string BillingAddress { get; set; }

        // Shipping address fields
        [NotMapped]
        public bool ShippingSameAsBilling { get; set; }

        [DisplayName("Shipping Country")]
        [RequiredIf("ShippingSameAsBilling", false, ErrorMessage = "Shipping Country is required")]
        public string ShippingCountry { get; set; }

        [DisplayName("Shipping City")]
        [RequiredIf("ShippingSameAsBilling", false, ErrorMessage = "Shipping City is required")]
        public string ShippingCity { get; set; }

        [DisplayName("Shipping Zip-Code")]
        [RequiredIf("ShippingSameAsBilling", false, ErrorMessage = "Shipping ZIP Code is required")]
        public string ShippingZipcode { get; set; }

        [DisplayName("Shipping Address")]
        [RequiredIf("ShippingSameAsBilling", false, ErrorMessage = "Shipping Address is required")]
        public string ShippingAddress { get; set; }
    }
}
