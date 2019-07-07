using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace PaymentGateway.Helpers
{
    public static class ConfigHelper
    {
        public static int GetCvvMinimumLength()
        {
            int fallbackValue = 3;

            try
            {
                return Convert.ToInt32(GetFromConfig(ConfigKeys.CVV_MIN_LENGTH));
            }
            catch (Exception ex)
            {

            }

            return fallbackValue;
        }
        public static int GetCvvMaximumLength()
        {
            int fallbackValue = 4;

            try
            {
                return Convert.ToInt32(GetFromConfig(ConfigKeys.CVV_MAX_LENGTH));
            }
            catch (Exception ex)
            {

            }

            return fallbackValue;
        }
        public static int GetCardNumberMaxLength()
        {
            int fallbackValue = 16;
            try
            {
                return Convert.ToInt32(GetFromConfig(ConfigKeys.CARD_NUM_MAX_LENGTH));
            }
            catch (Exception ex)
            {

            }

            return fallbackValue;
        }
        public static int GetCardNumberMinLength()
        {
            int fallbackValue = 13;
            try
            {
                return Convert.ToInt32(GetFromConfig(ConfigKeys.CARD_NUM_MIN_LENGTH));
            }
            catch (Exception ex)
            {

            }

            return fallbackValue;
        }

        private static object GetFromConfig(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }
    }
}