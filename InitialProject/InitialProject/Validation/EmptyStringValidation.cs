﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace InitialProject.Validation
{
    public class EmptyStringValidation : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            ValidationResult validationResult = new ValidationResult(false, "Value cannot be empty.");
            if (value != null)
            {
                string valueAsString = value as string;
                if (valueAsString.Length > 0)
                    validationResult = ValidationResult.ValidResult;
            }
            return validationResult;
        }
    }
}
