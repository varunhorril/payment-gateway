using AcquiringBank.Api.Client.Models.Constants;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AcquiringBank.Api.Client.Annotations
{
    public class GuidValidation : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            bool isGuid = Guid.TryParse(value.ToString(), out Guid result);
            if (isGuid)
            {
                return ValidationResult.Success;
            }

            return new ValidationResult(ApiMessages.FAIL);
        }
    }
}