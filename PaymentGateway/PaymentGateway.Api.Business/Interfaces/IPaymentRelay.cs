using PaymentGateway.Core.Models;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace PaymentGateway.Api.Business.Interfaces
{
    public interface IPaymentRelayBase
    {
        Payment Payment { get; set; }
        Task<IResponseBase> Relay();
        Task<IResponseBase> HandleResponse(HttpResponseMessage httpResponse);
        string GetBankApiEndpoint();
        string EncryptRequestContent();
        AuthenticationHeaderValue SetRequestBasicAuth();
    }
}
