using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentGateway.Core.Models
{
    public class Shopper : AuditBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid ShopperId { get; set; }
        [Required]
        [MaxLength(225)]
        public Guid CardId { get; set; }
        [Required]
        [MaxLength(225)]
        public string CardNumber { get; set; }
        [Required]
        public decimal Amount { get; set; }
        [Required]
        [MaxLength(25)]
        public string Currency { get; set; }
        [Required]
        [MaxLength(10)]
        public string CVVNumber { get; set; }
        [Required]
        [MaxLength(10)]
        public string CardExpiry { get; set; }

    }
}
