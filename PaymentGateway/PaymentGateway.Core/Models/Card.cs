using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentGateway.Core.Models
{
    public class Card : AuditBase
    {
        public Guid CardId { get; set; }
        public Guid CardTypeId { get; set; }
        public string Name { get; set; }
        public string IssuerName { get; set; }
    }
}
