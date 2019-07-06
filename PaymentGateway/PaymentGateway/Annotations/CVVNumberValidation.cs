using PaymentGateway.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PaymentGateway.Annotations
{
    public class CVVNumberValidation : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (IsValidNumber(value) && HasValidLength(value))
            {
                return true;
            }

            return false;
        }

        private bool IsValidNumber(object value)
        {
            return int.TryParse(value.ToString(), out int result);
        }

        private bool HasValidLength(object value)
        {
            var cvvLength = value.ToString().Length;
            var maxCvvLength = ConfigHelper.GetCvvMaximumLength();
            var minCvvLength = ConfigHelper.GetCvvMinimumLength();

            if (cvvLength >= minCvvLength && cvvLength <= maxCvvLength)
            {
                return true;
            }

            return false;
        }
    }
}