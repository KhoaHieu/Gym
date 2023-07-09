
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace GymManagement.ValidationRules
{
    public class PhoneValidationRules : ValidationRule
    {
        public const string motif = @"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$";


        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (!string.IsNullOrWhiteSpace((value ?? "").ToString()))
            {
                if (Regex.IsMatch((string)value, motif))
                {
                    return ValidationResult.ValidResult;
                }
                else return new ValidationResult(false, "Phone required");
            }
            else return new ValidationResult(false, "Field is required");
        }
    }
}
