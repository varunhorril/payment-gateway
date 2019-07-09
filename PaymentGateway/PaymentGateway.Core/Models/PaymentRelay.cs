using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentGateway.Core.Models
{
    public class PaymentRelay
    {
        public Guid PaymentId { get; set; }
        public string CardNumber { get; set; }
        public string CVVNumber { get; set; }
        public string CardExpiry { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public string MerchantName { get; set; }
        public string CardIssuerName { get; set; }
        public DateTime PaymentDate { get; set; }
    }
}
