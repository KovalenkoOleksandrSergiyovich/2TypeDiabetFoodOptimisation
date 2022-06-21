using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Controls;

namespace WpfApp2TypeDiabet.ValidationRules
{
    public class UserNameValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string input = value as string;
            string ErrorMessage;
            var hasUpperChar = new Regex(@"[A-Z]+");
            var hasLowerChar = new Regex(@"[a-z]+");
            var hasMiniMaxChars = new Regex(@".{4,20}");

            if (string.IsNullOrWhiteSpace(input) || string.IsNullOrEmpty(input))
            {
                ErrorMessage = "Ім'я користувача не може бути порожнім";
                return new ValidationResult(false, ErrorMessage);
            }
            else if (!hasUpperChar.IsMatch(input))
            {
                ErrorMessage = "Ім'я користувача повинно мати хоча б одну велику літеру";
                return new ValidationResult(false, ErrorMessage);
            }
            else if(!hasLowerChar.IsMatch(input))
            {
                ErrorMessage = "Ім'я користувача повинно мати хоча б одну малу літеру";
                return new ValidationResult(false, ErrorMessage);
            }
            else if(!hasMiniMaxChars.IsMatch(input))
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
