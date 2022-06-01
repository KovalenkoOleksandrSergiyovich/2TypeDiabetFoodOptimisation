using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace WpfApp2TypeDiabet.ValidationRules
{
    public class PriceValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string input = value as string;
            string ErrorMessage;
            var hasNumber = new Regex(@"[0-9]+");
            var hasCommaOrPeriod = new Regex(@"[,.]{1}");
            var hasTwoDecimals = new Regex(@"[0-9]{2}");

            if (string.IsNullOrEmpty(input) || string.IsNullOrWhiteSpace(input))
            {
                ErrorMessage = "Поле вартості товару не може бути порожнім";
                return new ValidationResult(false, ErrorMessage);
            }
            else if (!hasNumber.IsMatch(input))
            {
                ErrorMessage = "Вартість товару має бути числовим значенням";
                return new ValidationResult(false, ErrorMessage);
            }
            else if (hasCommaOrPeriod.IsMatch(input))
            {
                if (!hasTwoDecimals.IsMatch(input))
                {
                    ErrorMessage = "Після розділового знаку повинно бути два числових значення";
                    return new ValidationResult(false, ErrorMessage);
                }
                else
                {
                    return new ValidationResult(true, null);
                }
            }
            else
            {
                return new ValidationResult(true, null);
            }
        }
    }
}
