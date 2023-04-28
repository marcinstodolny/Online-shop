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
        [Required]
        public string Email { get; set; }
        [Required]
        public string Phone { get; set; }
        [Required]
        public string BillingCountry { get; set; }
        [Required]
        public string BillingCity { get; set; }
        [Required]
        public string BillingZipcode { get; set; }
        [Required]
        public string BillingAddress { get; set; }
        [Required]
        public string ShippingCountry { get; set; }
        [Required]
        public string ShippingCity { get; set; }
        [Required]
        public string ShippingZipcode { get; set; }
        [Required]
        public string ShippingAddress { get; set; }
    }
}
