using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Validators;
using Microsoft.VisualBasic;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Domain.Payments
{
    public class CreditCard
    {
        public bool PayWithCreditCard { get; set; }

        [DisplayName("Card number")]
        [Required(ErrorMessage = "Card number is required")]
        [RegularExpression("^[0-9]{16}$", ErrorMessage = "Please enter a valid 16-digit card number.")]
        public string CardNumber { get; set; }

        [DisplayName("Card Holder Name")]
        [Required(ErrorMessage = "Card holder's name is required")]
        public string CardHolder { get; set; }

        [Display(Name = "Expiry Date")]
        [Required(ErrorMessage = "Please enter the expiry date.")]
        [ValidateExpiryDate(ErrorMessage = "Expiry date cannot be in the past.")]
        public string ExpiryDate { get; set; }


        [Required(ErrorMessage = "Please enter a valid CVV code.")]
        [RegularExpression("^[0-9]{3}$", ErrorMessage = "Please enter a valid 3-digit CVV code.")]
        public int CVV { get; set; }
    }
}
