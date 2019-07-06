using PaymentGateway.Annotations;
using PaymentGateway.Attributes;
using PaymentGateway.Models.Constants;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PaymentGateway.Models.ViewModels
{
    public class PaymentViewModel
    {
        [Required(ErrorMessage = ApiMessages.MISSING_MERCHANT_ID)]
        [GuidValidation(ErrorMessage = ApiMessages.INVALID_MERCHANT_ID)]
        public string MerchantId { get; set; }

        [Required(ErrorMessage = ApiMessages.MISSING_CARD_NUMBER)]
        public string CardNumber { get; set; }

        [Required(ErrorMessage = ApiMessages.MISSING_CARD_EXPIRY)]
        public string ExpiryMonthDate { get; set; }

        [Required(ErrorMessage = ApiMessages.MISSING_CURRENCY)]
        public string Currency { get; set; }

        [Required(ErrorMessage = ApiMessages.MISSING_CVV_NUMBER)]
        [CVVNumberValidation(ErrorMessage =ApiMessages.MISSING_CVV_NUMBER)]
        public string CvvNumber { get; set; }
    }
}