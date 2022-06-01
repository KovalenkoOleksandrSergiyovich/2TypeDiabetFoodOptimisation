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
    public class UserNameValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string valueToValidate = value as string;
            string ErrorMessage;
            var hasUpperChar = new Regex(@"[A-Z]+");
            var hasLowerChar = new Regex(@"[a-z]+");
            var hasMiniMaxChars = new Regex(@".{4,20}");

            if (string.IsNullOrWhiteSpace(valueToValidate) || string.IsNullOrEmpty(valueToValidate))
            {
                ErrorMessage = "Ім'я користувача не може бути порожнім";
                return new ValidationResult(false, ErrorMessage);
            }
            else if (!hasUpperChar.IsMatch(valueToValidate /*"^[A-Za-z0-9._-]{4,20}$"*/))
            {
                ErrorMessage = "Ім'я користувача повинно мати хоча б одну велику літеру";
                return new ValidationResult(false, ErrorMessage);
            }
            else if(!hasLowerChar.IsMatch(valueToValidate))
            {
                ErrorMessage = "Ім'я користувача повинно мати хоча б одну малу літеру";
                return new ValidationResult(false, ErrorMessage);
            }
            else if(!hasMiniMaxChars.IsMatch(valueToValidate))
            {
                ErrorMessage = "Ім'я користувача не можу бути коротше 4 та довше 20 символів";
                return new ValidationResult(false, ErrorMessage);
            }
            else
            {
                return new ValidationResult(true, null);
            }
            
        }
    }
}
