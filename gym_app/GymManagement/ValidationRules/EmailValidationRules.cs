using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace GymManagement.ValidationRules
{
    class EmailValidationRules : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (!string.IsNullOrWhiteSpace((value ?? "").ToString()))
            {
                try
                {
                    MailAddress m = new MailAddress((string)value);

                    return ValidationResult.ValidResult;
                }
                catch (FormatException)
                {
                    return new ValidationResult(false, "Email required");
                }
            }
            else return ValidationResult.ValidResult;
            
        }
    }
}
