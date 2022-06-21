using System;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Controls;

namespace WpfApp2TypeDiabet.ValidationRules
{
    internal class PasswordValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string input = value as string;
            string ErrorMessage;

            if (string.IsNullOrWhiteSpace(input) || string.IsNullOrEmpty(input))
            {
                ErrorMessage = "Пароль не може бути порожнім";
                return new ValidationResult(false, ErrorMessage);
            }

            var hasNumber = new Regex(@"[0-9]+");
            var hasUpperChar = new Regex(@"[A-Z]+");
            var hasMiniMaxChars = new Regex(@".{8,15}");
            var hasLowerChar = new Regex(@"[a-z]+");
            var hasSymbols = new Regex(@"[!@#$%^&*()_+=\[{\]};:<>|./?,-]");

            if (!hasLowerChar.IsMatch(input))
            {
                ErrorMessage = "Пароль повинен містити хоча б одну малу літеру";
                return new ValidationResult(false, ErrorMessage);
            }
            else if (!hasUpperChar.IsMatch(input))
            {
                ErrorMessage = "Пароль повинен містити хоча б одну велику літеру";
                return new ValidationResult(false, ErrorMessage);
            }
            else if (!hasMiniMaxChars.IsMatch(input))
            {
                ErrorMessage = "Пароль не може бути менше 8 та більше 15 символів";
                return new ValidationResult(false, ErrorMessage);
            }
            else if (!hasNumber.IsMatch(input))
            {
                ErrorMessage = "Пароль повинен містити хоча б одне числове значення (0-9)";
                return new ValidationResult(false, ErrorMessage);
            }

            else if (!hasSymbols.IsMatch(input))
            {
                ErrorMessage = "Пароль повинен містити хоча б один спеціальний символ (!@#$%^&*()_+=[{]};:<>|./?,-)";
                return new ValidationResult(false, ErrorMessage);
            }
            else
            {
                return new ValidationResult(true, null);
            }
            throw new NotImplementedException();
        }
    }
}
