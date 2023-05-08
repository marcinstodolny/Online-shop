using System.ComponentModel.DataAnnotations;

namespace Domain.Validators
{
    public class ValidatePhone : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value == null) return true;
            var phone = value.ToString();
            return phone.Length >= 6 && phone.All(char.IsDigit);
        }
    }
}
