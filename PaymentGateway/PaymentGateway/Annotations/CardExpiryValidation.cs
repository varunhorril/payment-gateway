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

        public override bool IsValid(object value)
        {
            var trimmedValue = StringUtility.RemoveWhitespace(value.ToString());
            if (IsLengthValid(trimmedValue))
            {
                string[] splitValues = trimmedValue.Split(new[] { "/" }, StringSplitOptions.RemoveEmptyEntries);
                if (IsMonthValid(splitValues) && IsYearValid(splitValues))
                {
                    return true;
                }
            }

            return false;
        }

        private bool IsLengthValid(string value)
        {
            //Format: dd/yy or dd/yyyy
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

            return monthRegexPattern.Match(monthSegment).Success;
        }

        private bool IsYearValid(string[] splitValues)
        {
            var yearSegment = splitValues[1];
            var yearValue = Convert.ToInt32(yearSegment);
            if (yearValue > DateTime.UtcNow.Year)
            {
                return true;
            }

            return false;
        }
    }
}