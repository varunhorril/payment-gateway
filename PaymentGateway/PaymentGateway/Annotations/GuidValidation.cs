using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PaymentGateway.Attributes
{
    public class GuidValidation : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            bool isGuid = Guid.TryParse(value.ToString(), out Guid result);
            if (isGuid)
            {
                return true;
            }
            
            return false;
        }
    }
}