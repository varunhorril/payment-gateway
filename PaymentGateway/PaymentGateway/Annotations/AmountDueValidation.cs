using NLog;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PaymentGateway.Annotations
{
    public class AmountDueValidation : ValidationAttribute
    {
        private static Logger _logger = LogManager.GetCurrentClassLogger();

        public override bool IsValid(object value)
        {
            try
            {
                return IsAmountValid(value);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"[AmountDueValidation][IsValid] FAILED: {ex.Message}");
            }

            return false;
        }

        private bool IsAmountValid(object value)
        {
            var amount = Convert.ToDecimal(value);

            return amount > 0;
        }
    }
}