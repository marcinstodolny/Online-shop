using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace Domain.Validators
{
    public class ValidateExpiryDate : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value == null) return true;

            // Parse the expiry date string into a DateTime object using the TryParseExact method.
            var dateString = value.ToString();
            if (!DateTime.TryParseExact(dateString, "MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime expiryDate))
            {
                return false;
            }

            // Compare the expiry date with the current date to ensure that it's not in the past.
            return expiryDate > DateTime.Now.Date;
        }
    }
}