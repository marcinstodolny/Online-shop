using Codecool.CodecoolShop.Validators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        [Required(ErrorMessage = "Billing country is required")]
        public string BillingCountry { get; set; }
        [Required(ErrorMessage = "Billing City is required")]
        public string BillingCity { get; set; }
        [Required(ErrorMessage = "Billing Zip-Code is required")]
        public string BillingZipcode { get; set; }
        [Required(ErrorMessage = "Billing Address is required")]
        public string BillingAddress { get; set; }

        // Shipping address fields
        public bool ShippingSameAsBilling { get; set; }

        [RequiredIf("ShippingSameAsBilling", false, ErrorMessage = "Shipping Country is required")]
        public string ShippingCountry { get; set; }

        [RequiredIf("ShippingSameAsBilling", false, ErrorMessage = "Shipping City is required")]
        public string ShippingCity { get; set; }

        [RequiredIf("ShippingSameAsBilling", false, ErrorMessage = "Shipping ZIP Code is required")]
        public string ShippingZipcode { get; set; }

        [RequiredIf("ShippingSameAsBilling", false, ErrorMessage = "Shipping Address is required")]
        public string ShippingAddress { get; set; }
    }
}
