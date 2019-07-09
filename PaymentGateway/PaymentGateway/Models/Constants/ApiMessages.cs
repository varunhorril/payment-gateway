using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PaymentGateway.Models.Constants
{
    /// <summary>
    /// Holds all possible StringContent to return as API Responses.
    /// </summary>
    public static class ApiMessages
    {
        #region Null-check Messages
        public const string MISSING_MERCHANT_ID = "MISSING_MERCHANT_ID";
        public const string MISSING_CARD_NUMBER = "MISSING_CARD_NUMBER";
        public const string MISSING_CARD_EXPIRY = "MISSING_CARD_EXPIRY";
        public const string MISSING_AMOUNT = "MISSING_AMOUNT";
        public const string MISSING_CURRENCY = "MISSING_CURRENCY";
        public const string MISSING_CVV_NUMBER = "MISSING_CVV_NUMBER";
        public const string MISSING_CARD_ISSUER_NAME = "MISSING_CARD_ISSUER_NAME";
        #endregion

        #region Validity-check Messages
        public const string INVALID_MERCHANT_ID = "INVALID_MERCHANT_ID";
        public const string INVALID_CARD_NUMBER = "INVALID_CARD_NUMBER";
        public const string INVALID_AMOUNT = "INVALID_AMOUNT";
        public const string INVALID_CVV_NUMBER = "INVALID_CVV_NUMBER";
        public const string INVALID_CARD_EXPIRY = "INVALID_CARD_EXPIRY";
        public const string INVALID_CURRENCY = "INVALID_CURRENCY";
        public const string INVALID_CARD_ISSUER_NAME = "INVALID_CARD_ISSUER_NAME";
        #endregion

        public const string OK = "OK";
        public const string FAILED = "FAILED";

        public const string AUTH_FAILED = "AUTH_FAILED";
        public const string SERVER_ERROR = "SERVER_ERROR";

    }
}