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
        [Required(AllowEmptyStrings = false, ErrorMessage = ApiMessages.MISSING_MERCHANT_ID)]
        [GuidValidation(ErrorMessage = ApiMessages.INVALID_MERCHANT_ID)]
        public string MerchantId { get; set; }


        [Required(AllowEmptyStrings = false, ErrorMessage = ApiMessages.MISSING_CARD_NUMBER)]
        [StringLength(16, MinimumLength = 13, ErrorMessage = ApiMessages.INVALID_CARD_NUMBER)]
        public string CardNumber { get; set; }


        [Required(AllowEmptyStrings = false, ErrorMessage = ApiMessages.MISSING_CARD_EXPIRY)]
        [CardExpiryValidation(ErrorMessage = ApiMessages.INVALID_CARD_EXPIRY)]
        public string ExpiryMonthDate { get; set; }


        [Required(AllowEmptyStrings = false, ErrorMessage = ApiMessages.MISSING_CVV_NUMBER)]
        [CVVNumberValidation(ErrorMessage =ApiMessages.INVALID_CVV_NUMBER)]
        public string CvvNumber { get; set; }


        [Required(AllowEmptyStrings = false, ErrorMessage = ApiMessages.MISSING_CURRENCY)]
        [CurrencyValidation(ErrorMessage = ApiMessages.INVALID_CURRENCY)]
        public string Currency { get; set; }


        [Required(ErrorMessage = ApiMessages.MISSING_AMOUNT)]
        [AmountDueValidation(ErrorMessage = ApiMessages.INVALID_AMOUNT)]
        public decimal Amount { get; set; }


        [Required(AllowEmptyStrings = false, ErrorMessage = ApiMessages.INVALID_CARD_ISSUER_NAME)]
        public string CardIssuerName { get; set; }


        [Required(AllowEmptyStrings = false, ErrorMessage = ApiMessages.MISSING_BANK_NAME)]
        public string BankName { get; set; }
    }
}