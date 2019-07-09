using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentGateway.Core.Models
{
    public class Payment : AuditBase
    {
        public Guid PaymentId { get; set; }
        public Guid MerchantId { get; set; }
        public Guid BankId { get; set; }
        public Guid ShopperId { get; set; }
        public DateTime TransactionTimeUtc { get; set; }
        public string TransactionId { get; set; }
        public string PaymentRelayStatus { get; set; }

    }
}
