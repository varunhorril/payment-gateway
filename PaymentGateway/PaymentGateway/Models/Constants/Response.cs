using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PaymentGateway.Api.Business;
using PaymentGateway.Api.Business.Interfaces;

namespace PaymentGateway.Models.Constants
{
    public class Response : IResponseBase
    {
        public bool IsSuccessful { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }

        public Response()
        {
            IsSuccessful = false;
        }
    }
}