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

        }
    }
}