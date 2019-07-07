using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentGateway.Core.Models
{
    public class CardType : AuditBase
    {
        public Guid CardTypeId { get; set; }
        public int CardNumberLength { get; set; }
        public int MIILength { get; set; }
        public int IINLength { get; set; }
        public string ValidationMethod { get; set; }
    }
}
