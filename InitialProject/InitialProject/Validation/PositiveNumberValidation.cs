using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace InitialProject.Validation
{
    public class PositiveNumberValidation : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value == null)
            {
                return new ValidationResult(false, "Value cannot be null.");
            }

            if (!int.TryParse(value.ToString(), out int number))
            {
                return new ValidationResult(false, "Value must be a valid number.");
            }

            if (number <= 0)
            {
                return new ValidationResult(false, "Value must be greater than zero.");
            }

            return ValidationResult.ValidResult;
        }
    }
}
