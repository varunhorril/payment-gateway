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
    }
}