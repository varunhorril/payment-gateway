using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentGateway.Core.Models
{
    public class Payment : AuditBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid PaymentId { get; set; }
        [Required]
        public Guid MerchantId { get; set; }
        [Required]
        public Guid BankId { get; set; }
        [Required]
        public Guid ShopperId { get; set; }
        [Required]
        public DateTime TransactionTimeUtc { get; set; }
        [MaxLength(225)]
        public string TransactionId { get; set; }
        [MaxLength(25)]
        public string PaymentRelayStatus { get; set; }

    }
}
