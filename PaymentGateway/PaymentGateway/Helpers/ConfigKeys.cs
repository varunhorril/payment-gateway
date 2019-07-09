using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PaymentGateway.Helpers
{
    /// <summary>
    /// Holds keys for AppSettings in Web.config.
    /// </summary>
    public static class ConfigKeys
    {
        public const string CVV_MIN_LENGTH = "CVV_MIN_LENGTH";
        public const string CVV_MAX_LENGTH = "CVV_MAX_LENGTH";

        public const string CARD_NUM_MAX_LENGTH = "CARD_NUM_MAX_LENGTH";
        public const string CARD_NUM_MIN_LENGTH = "CARD_NUM_MIN_LENGTH";

        public const string AUTH_USER = "AUTH_USER";
        public const string AUTH_PASS = "AUTH_PASS";

        public const string API_CLIENT_URL = "ApiClientUrl";

    }
}