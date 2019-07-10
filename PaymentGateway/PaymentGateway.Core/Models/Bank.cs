using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentGateway.Core.Models
{
    public class Bank : AuditBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid BankId { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        public string CountryOfRegistration { get; set; }

    }
}
