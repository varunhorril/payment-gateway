using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentGateway.Core.Models
{
    public class Merchant : AuditBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid MerchantId { get; set; }
        [Required]
        [MaxLength(225)]
        public string AuthSalt { get; set; }
        [Required]
        [MaxLength(225)]
        public string Name { get; set; }
        [MaxLength(50)]
        public string CountryOfRegistration { get; set; }
        public DateTime RegistrationExpiry { get; set; }
    }
}
