using PaymentGateway.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace PaymentGateway.Annotations
{
    public class CardExpiryValidation : ValidationAttribute
    {
        private const string MONTH_REGEX = @"^(0[1-9]|1[0-2])$";

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var trimmedValue = StringUtility.RemoveWhitespace(value.ToString());
            if (IsLengthValid(trimmedValue))
            {
                string[] splitValues = trimmedValue.Split(new[] { "/" }, StringSplitOptions.RemoveEmptyEntries);
                if (IsMonthValid(splitValues) && IsYearValid(splitValues))
                {
                    return ValidationResult.Success;
                }
            }

            return new ValidationResult(ErrorMessage);
        }

        private bool IsLengthValid(string value)
        {
            //Format: mm/yy or mm/yyyy
            if (!string.IsNullOrWhiteSpace(value) && (value.Length == 5 || value.Length == 7))
            {
                return true;
            }

            return false;
        }

        private bool IsMonthValid(string[] splitValues)
        {
            var monthSegment = splitValues[0];
            var monthRegexPattern = new Regex(MONTH_REGEX, RegexOptions.None);
            if (monthRegexPattern.Match(monthSegment).Success)
            {
                var currentMonth = DateTime.UtcNow.Month;
                if (Convert.ToInt32(monthSegment) >= currentMonth)
                {
                    return true;
                }
            }

            return false;
        }

        private bool IsYearValid(string[] splitValues)
        {
            var yearSegment = splitValues[1];

            //Validation for Year {yyyy}
            if (yearSegment.Length == 4)
            {
                var yearValue = Convert.ToInt32(yearSegment);
                if (yearValue >= DateTime.UtcNow.Year)
                {
                    return true;
                }
            }

            //Validation for Year {yy}
            if (yearSegment.Length == 2)
            {
                int currentYearDigit = DateTime.UtcNow.Year % 10;
                int inputYearDigit = Convert.ToInt32(yearSegment);
                if (inputYearDigit >= currentYearDigit)
                {
                    return true;
                }
            }
            

            return false;
        }
    }
}