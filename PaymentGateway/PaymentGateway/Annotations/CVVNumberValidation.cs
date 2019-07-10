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
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (IsValidNumber(value) && HasValidLength(value))
            {
                return ValidationResult.Success;
            }

            return new ValidationResult(ErrorMessage);
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