using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentGateway.Core.Models
{
    public class Bank : AuditBase
    {
        public Guid BankId { get; set; }
        public string Name { get; set; }
        public string CountryOfRegistration { get; set; }

    }
}
