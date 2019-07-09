using PaymentGateway.Api.Business.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PaymentGateway.Modules.Payment
{
    /// <summary>
    /// Sends payment to bank API Endpoint
    /// </summary>
    public class PaymentRelayModule : IPaymentRelay
    {
        public IResponseBase Relay()
        {
            throw new NotImplementedException();
        }

        public void SetRequestBasicAuth()
        {
            throw new NotImplementedException();
        }

        public string GetBankApiEndpoint()
        {
            throw new NotImplementedException();
        }

        public string EncryptRequestContent()
        {
            throw new NotImplementedException();
        }

        public IResponseBase HandleResponse()
        {
            throw new NotImplementedException();
        }
    }
}