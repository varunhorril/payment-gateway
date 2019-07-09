using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PaymentGateway.Models.Constants
{
    /// <summary>
    /// API Client response 
    /// </summary>
    public class ClientResponse
    {
        public string TransactionId { get; set; }
        public string Status { get; set; }
    }
}