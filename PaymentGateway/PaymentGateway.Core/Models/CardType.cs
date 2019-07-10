using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentGateway.Core.Models
{
    public class CardType : AuditBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid CardTypeId { get; set; }
        [Required]
        public int CardNumberLength { get; set; }
        [Required]
        public int MIILength { get; set; }
        [Required]
        public int IINLength { get; set; }
        public string ValidationMethod { get; set; }
    }
}
