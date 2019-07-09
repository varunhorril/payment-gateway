using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentGateway.Api.Business.Interfaces
{
    public interface IPaymentRelay
    {
        IResponseBase Relay();
        IResponseBase HandleResponse();
        string GetBankApiEndpoint();
        string EncryptRequestContent();
        void SetRequestBasicAuth();
    }
}
