using PaymentGateway.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentGateway.Api.Business.Interfaces
{
    public interface IPaymentRelay
    {
        Payment Payment { get; set; }
        IResponseBase Relay();
        IResponseBase HandleResponse();
        string GetBankApiEndpoint();
        string EncryptRequestContent();
        void SetRequestBasicAuth();
    }
}
