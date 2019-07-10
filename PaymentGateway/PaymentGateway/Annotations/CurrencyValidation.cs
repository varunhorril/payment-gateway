using NLog;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PaymentGateway.Annotations
{
    public class CurrencyValidation : ValidationAttribute
    {
        private static Logger _logger = LogManager.GetCurrentClassLogger();

        private IList<string> CurrenciesList = new List<string>
        {
            "USD", "EUR", "INR", "RUB", "NZD", "AUD", "CAD", "MUR"
        };

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            try
            {
                if (value != null && IsValidCurrency(value))
                {
                    return ValidationResult.Success;
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"[CurrencyValidation][IsValid] {ex.Message}");
            }

            return new ValidationResult(ErrorMessage);
        }

        private bool IsValidCurrency(object value)
        {
            string currency = value.ToString();
            return CurrenciesList.Any(c => c.Trim()
                                            .Equals(currency, StringComparison.OrdinalIgnoreCase));
        }
    }
}