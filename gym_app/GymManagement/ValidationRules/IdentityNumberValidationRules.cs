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
    public class IdentityNumberValidationRules : ValidationRule
    {
        public const string motif = @"^[0-9]{12}";
        public const string motif2 = @"^[a-zA-Z]+$";
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (!string.IsNullOrWhiteSpace((value ?? "").ToString()))
            {
                if (Regex.IsMatch((string)value, motif))
                {
                    return ValidationResult.ValidResult;
                }
                else return new ValidationResult(false, "Number only");
            }
            else return new ValidationResult(false, "Field is required");
        }
    }
}

