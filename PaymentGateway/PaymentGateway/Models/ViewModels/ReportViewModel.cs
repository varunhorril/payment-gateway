using PaymentGateway.Attributes;
using PaymentGateway.Models.Constants;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PaymentGateway.Models.ViewModels
{
    public class ReportRequestViewModel
    {
        [Required(ErrorMessage = ApiMessages.MISSING_PAYMENT_ID)]
        [GuidValidation(ErrorMessage = ApiMessages.INVALID_PAYMENT_ID)]
        public string PaymentId { get; set; }
    }
}