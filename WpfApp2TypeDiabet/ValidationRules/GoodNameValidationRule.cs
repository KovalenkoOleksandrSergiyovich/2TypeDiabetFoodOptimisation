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
    public class GoodNameValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string input = value as string;
            string ErrorMessage;
            var hasAllNessesary = new Regex(@"^([А-Яа-яІЇЄҐіїєґ0-9()'`., %]{2,})$");
            if(string.IsNullOrEmpty(input) || string.IsNullOrWhiteSpace(input))
            {
                ErrorMessage = "Назва продукту не може бути порожньою";
                return new ValidationResult(false, ErrorMessage);
            }
            else if(hasAllNessesary.IsMatch(input))
            {
                ErrorMessage = "Назва продукту не може бути менше двох символів";
                return new ValidationResult(false, ErrorMessage);
            }
            else
            {
                return new ValidationResult(true, null);
            }
        }
    }
}
