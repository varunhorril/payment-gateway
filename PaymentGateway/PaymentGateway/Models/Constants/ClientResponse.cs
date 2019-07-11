using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace PaymentGateway.Models.Constants
{
    /// <summary>
    /// API Client response 
    /// </summary>
    public class ClientResponse
    {
        public string TransactionId { get; set; }
        public HttpStatusCode Status { get; set; }
    }
}