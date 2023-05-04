using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

namespace Domain.Payments
{
    [NotMapped]
    public class CreditCard
    {
        [DisplayName("Card number")]
        [Required(ErrorMessage = "Card number is required")]
        public int CardNumber { get; set; }

        [DisplayName("Card Holder Name")]
        [Required(ErrorMessage = "Card holder's name is required")]
        public string CardHolder { get; set; }

        [DisplayName("Expiry Date")]
        [Required(ErrorMessage = "Expiry date is required")]
        public DateOnly ExpiryDate { get; set; }

        [Required(ErrorMessage = "CVV code (from the back of the card) is required")]
        public int CVV { get; set; }
    }
}
