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
                var configValue = ConfigurationManager.AppSettings[ConfigKeys.CVV_MIN_LENGTH];

                return Convert.ToInt32(configValue);
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
                var configValue = ConfigurationManager.AppSettings[ConfigKeys.CVV_MAX_LENGTH];

                return Convert.ToInt32(configValue);
            }
            catch (Exception ex)
            {

            }

            return fallbackValue;
        }
    }
}