using System.ComponentModel.DataAnnotations;

namespace Codecool.CodecoolShop.Validators
{
    [AttributeUsage(AttributeTargets.Property)]
    public class RequiredIfAttribute : ValidationAttribute
    {
        private readonly string _comparisonProperty;
        private readonly object _comparisonValue;

        public RequiredIfAttribute(string comparisonProperty, object comparisonValue)
        {
            _comparisonProperty = comparisonProperty;
            _comparisonValue = comparisonValue;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var property = validationContext.ObjectType.GetProperty(_comparisonProperty);

            if (property == null)
            {
                return new ValidationResult($"Unknown property {_comparisonProperty}");
            }

            var comparisonValue = property.GetValue(validationContext.ObjectInstance, null);

            if (!comparisonValue.Equals(_comparisonValue))
            {
                return ValidationResult.Success;
            }

            if (value == null || string.IsNullOrWhiteSpace(value.ToString()))
            {
                return new ValidationResult($"{validationContext.DisplayName} is required.");
            }

            return ValidationResult.Success;
        }
        
    }
}
