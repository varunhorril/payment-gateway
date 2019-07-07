using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentGateway.Core.Models
{
    public class Merchant : AuditBase
    {
        public Guid MerchantId { get; set; }
        public string Name { get; set; }
        public string CountryOfRegistration { get; set; }
        public DateTime RegistrationExpiry { get; set; }
    }
}
