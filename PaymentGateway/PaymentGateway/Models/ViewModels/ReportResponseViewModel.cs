using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PaymentGateway.Models.ViewModels
{
    public class ReportResponseViewModel
    {
        public Guid PaymentId { get; set; }
        public string MaskedCardNumber { get; set; }
        public string CVVNumber { get; set; }
        public string CardExpiry { get; set; }
        public string PaymentProcessStatus { get; set; }
        public string DateProcessed { get; set; }
    }
}