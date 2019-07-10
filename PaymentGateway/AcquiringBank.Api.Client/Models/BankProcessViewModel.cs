using AcquiringBank.Api.Client.Annotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AcquiringBank.Api.Client.Models
{
    public class BankProcessViewModel
    {
        [Required]
        [GuidValidation]
        public string PaymentId { get; set; }

        [Required]
        [StringLength(16)]
        public string CardNumber { get; set; }

        [Required]
        [StringLength(5)]
        public string CVVNumber { get; set; }

        [Required]
        public string CardExpiry { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [Required]
        [StringLength(5)]
        public string Currency { get; set; }

        [Required]
        [StringLength(30)]
        public string MerchantName { get; set; }

        [Required]
        [StringLength(30)]
        public string CardIssuerName { get; set; }

        [Required]
        public string PaymentDate { get; set; }

    }
}