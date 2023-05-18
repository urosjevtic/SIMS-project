using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace InitialProject.Validation
{
    public class ImagesUrlValidation : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            try
            {
                var s = value as string;
                Regex validUrlRegex = new Regex(@"((?:https?:\/\/)?[a-zA-Z0-9_\-]+\.[a-zA-Z0-9_\-]+\.[a-zA-Z0-9_\-]+),?");
                string pattern = @"^(?:\s*\w+\s*,\s*)*\s*\w+\s*(?:,\s*\w+\s*)*$";
                if (validUrlRegex.IsMatch(s))
                {
                    return new ValidationResult(true, null);
                }

                return new ValidationResult(false, "Incorrect format.");
            }
            catch
            {
                return new ValidationResult(false, "Unknown error occured.");
            }
        }
    }
}
