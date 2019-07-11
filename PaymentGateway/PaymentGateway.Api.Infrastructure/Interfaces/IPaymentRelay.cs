using PaymentGateway.Core.Models;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace PaymentGateway.Api.Business.Interfaces
{
    public interface IPaymentRelayBase
    {
        Payment Payment { get; set; }
        Task<IResponseBase> RelayAsync();
        Task<IResponseBase> HandleResponseAsync(HttpResponseMessage httpResponse);
        IResponseBase Relay();
        string GetBankApiEndpoint();
        AuthenticationHeaderValue SetRequestBasicAuth();
    }
}
