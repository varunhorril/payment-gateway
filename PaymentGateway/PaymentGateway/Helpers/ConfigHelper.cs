using NLog;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace PaymentGateway.Helpers
{
    public static class ConfigHelper
    {
        private static Logger _logger = LogManager.GetCurrentClassLogger();

        public static int GetCvvMinimumLength()
        {
            int fallbackValue = 3;

            try
            {
                return Convert.ToInt32(GetFromConfig(ConfigKeys.CVV_MIN_LENGTH));
            }
            catch (Exception ex)
            {
                _logger.Warn(ex, "[ConfigHelper][GetCvvMinimumLength] : Value not present in Web.config.");
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
                _logger.Warn(ex, "[ConfigHelper][GetCvvMaximumLength] : Value not present in Web.config.");

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
                _logger.Warn(ex, "[ConfigHelper][GetCardNumberMaxLength] : Value not present in Web.config.");
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
                _logger.Warn(ex, "[ConfigHelper][GetCardNumberMinLength] : Value not present in Web.config.");

            }

            return fallbackValue;
        }
        public static string GetAuthUser()
        {
            var user = string.Empty;

            try
            {
                return GetFromConfig(ConfigKeys.AUTH_USER).ToString();

            }
            catch (Exception ex)
            {
                _logger.Warn(ex, "[ConfigHelper][GetAuthUser] : Value not present in Web.config.");
            }

            return user;
        }
        public static string GetAuthPass()
        {
            try
            {
                return GetFromConfig(ConfigKeys.AUTH_PASS).ToString();

            }
            catch (Exception ex)
            {
                _logger.Warn(ex, "[ConfigHelper][GetAuthPass] : Value not present in Web.config.");
            }

            return string.Empty;
        }
        public static string GetApiClientUrl()
        {
            try
            {
                return GetFromConfig(ConfigKeys.API_CLIENT_URL).ToString();

            }
            catch (Exception ex)
            {
                _logger.Warn(ex, "[ConfigHelper][GetApiClientUrl] : " +
                            "API Client URL not present in Web.config.");
            }

            return string.Empty;
        }

        private static object GetFromConfig(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }
    }
}