using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace GymManagement.ValidationRules
{
    class WordOnlyValidationRules : ValidationRule
    {
        public const string motif = @"[^0-9]+$";
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (!string.IsNullOrWhiteSpace((value ?? "").ToString()))
            {
                if (Regex.IsMatch((string)value, motif))
                {
                    return ValidationResult.ValidResult;
                }
                else return new ValidationResult(false, "Word only");
            }
            else return new ValidationResult(false, "Field is required");
        }
    }
}
