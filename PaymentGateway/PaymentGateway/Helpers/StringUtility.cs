using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PaymentGateway.Helpers
{
    public static class StringUtility
    {
        public static string RemoveWhitespace(string content)
        {
            return string.Join("", content.Split(default(string[]), StringSplitOptions.RemoveEmptyEntries));
        }
        public static bool IsValidGuid(string guidString)
        {
            var isGuid = Guid.TryParse(guidString, out Guid result);
            if (isGuid && !guidString.Equals(Guid.Empty.ToString()))
            {
                return true;
            }

            return false;
        }
        public static Guid ConvertToGuid(string guidString)
        {
            return Guid.Parse(guidString);
        }
        public static string GetMaskedCardNumber(string cardNumber)
        {
            var trimmedCardNum = RemoveWhitespace(cardNumber);
            var firstUnmaskedDigits = trimmedCardNum.Substring(0, 5);
            var lastUnmaskedDigits = trimmedCardNum.Substring(trimmedCardNum.Length - 4, 4);

            var maskedDigits = new String('*', trimmedCardNum.Length - (firstUnmaskedDigits.Length + lastUnmaskedDigits.Length));

            return string.Concat(firstUnmaskedDigits, maskedDigits, lastUnmaskedDigits);
        }
    }
}