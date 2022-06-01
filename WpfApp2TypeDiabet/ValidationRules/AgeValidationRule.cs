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
    internal class AgeValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string input = value as string;
            string ErrorMessage;
            var hasNumber = new Regex(@"[0-9]+");

            if (string.IsNullOrEmpty(input) || string.IsNullOrWhiteSpace(input))
            {
                ErrorMessage = "Поле віку не може бути порожнім";
                return new ValidationResult(false, ErrorMessage);
            }
            else if(!hasNumber.IsMatch(input))
            {
                ErrorMessage = "Вік має бути числовим значенням";
                return new ValidationResult(false, ErrorMessage);
            }
            else if(Int32.Parse(input) < 4)
            {
                ErrorMessage = "На жаль, система не має рекомендацій щодо харчування в такому віці";
                return new ValidationResult(false, ErrorMessage);
            }
            else if (Int32.Parse(input) > 200)
            {
                ErrorMessage = "На жаль, система не має рекомендацій щодо харчування в такому віці";
                return new ValidationResult(false, ErrorMessage);
            }
            else
            {
                return new ValidationResult(true, null);
            }
        }
    }
}
