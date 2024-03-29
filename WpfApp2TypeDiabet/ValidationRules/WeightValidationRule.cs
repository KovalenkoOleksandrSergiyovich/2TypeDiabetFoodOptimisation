﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace WpfApp2TypeDiabet.ValidationRules
{
    internal class WeightValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string input = value as string;
            string ErrorMessage;
            var hasNumber = new Regex(@"[0-9]+");

            if (string.IsNullOrEmpty(input) || string.IsNullOrWhiteSpace(input))
            {
                ErrorMessage = "Поле ваги не може бути порожнім";
                return new ValidationResult(false, ErrorMessage);
            }
            else if (!hasNumber.IsMatch(input))
            {
                ErrorMessage = "Вага має бути числовим значенням";
                return new ValidationResult(false, ErrorMessage);
            }
            else if (!(double.Parse(input, CultureInfo.InvariantCulture) > 0))
            {
                ErrorMessage = "Вага не може дорівнювати нулю";
                return new ValidationResult(false, ErrorMessage);
            }
            else if (double.Parse(input, CultureInfo.InvariantCulture) > 1000)
            {
                ErrorMessage = "Завелике значення ваги";
                return new ValidationResult(false, ErrorMessage);
            }
            else
            {
                return new ValidationResult(true, null);
            }
        }
    }
}
