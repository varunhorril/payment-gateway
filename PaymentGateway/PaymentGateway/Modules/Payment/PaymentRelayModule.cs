using Newtonsoft.Json;
using NLog;
using PaymentGateway.Api.Business.Interfaces;
using PaymentGateway.Api.Infrastructure.DAL.Repositories;
using PaymentGateway.Core.Exceptions;
using PaymentGateway.Core.Models;
using PaymentGateway.Helpers;
using PaymentGateway.Models.Constants;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace PaymentGateway.Modules.Payment
{
    /// <summary>
    /// Sends payment asynchronously to client API Endpoint
    /// </summary>
    public class PaymentRelayModule : IPaymentRelayBase
    {
        public Core.Models.Payment Payment { get; set; }
        public Core.Models.Card Card { get; set; }
        public Core.Models.Shopper Shopper { get; set; }
        public Core.Models.Merchant Merchant { get; set; }

        private static Logger _logger = LogManager.GetCurrentClassLogger();
        private static readonly HttpClient _httpClient = new HttpClient();
        private const string BASIC_AUTH_SCHEME = "Basic";

        public async Task<IResponseBase> Relay()
        {
            IResponseBase response = new Response();

            try
            {
                using (var httpRequest = new HttpRequestMessage(HttpMethod.Post, GetBankApiEndpoint()))
                {
                    httpRequest.Content = new StringContent(GetRequestBody());
                    _httpClient.DefaultRequestHeaders.Authorization = SetRequestBasicAuth();

                    var httpResponse = await _httpClient.SendAsync(httpRequest);

                    response = await HandleResponse(httpResponse);
                }
            }
            catch (WebException webEx)
            {
                _logger.Error(webEx, $"[PaymentRelayModule][Relay] REQUEST FAILED: {webEx.Message}");
            }
            catch (RelayException ex)
            {
                _logger.Error(ex, $"[PaymentRelayModule][Relay] FAILED: {ex.Message}");
            }

            return response;
        }

        public async Task<IResponseBase> HandleResponse(HttpResponseMessage httpResponse)
        {
            IResponseBase result = new Response();
            var responseString = await httpResponse.Content.ReadAsStringAsync();
            var clientResponse = JsonConvert.DeserializeObject<ClientResponse>(responseString);

            if (!httpResponse.IsSuccessStatusCode)
            {
                UpdatePayment(clientResponse);
                return result;
            }

            UpdatePayment(clientResponse, true);

            return result;
        }

        public AuthenticationHeaderValue SetRequestBasicAuth()
        {
            var username = ConfigHelper.GetAuthUser();
            var password = GetAuthPass();
            var authByteArray = Encoding.ASCII.GetBytes($"{username}:{password}");

            return new AuthenticationHeaderValue(BASIC_AUTH_SCHEME, Convert.ToBase64String(authByteArray));
        }

        public string GetBankApiEndpoint()
        {
            return ConfigHelper.GetApiClientUrl();
        }

        public string EncryptRequestContent()
        {
            throw new NotImplementedException();
        }

        private string GetRequestBody()
        {
            var paymentRelay = new PaymentRelay()
            {
                PaymentId = Payment.PaymentId,
                CardNumber = Shopper.CardNumber,
                CardIssuerName = Card.IssuerName,
                CVVNumber = Shopper.CVVNumber,
                CardExpiry = Shopper.CardExpiry,
                Amount = Shopper.Amount,
                Currency = Shopper.Currency,
                MerchantName = Merchant.Name,
                PaymentDate = Payment.CreatedOn
            };

            var requestBody = JsonConvert.SerializeObject(paymentRelay);

            return requestBody;
        }

        private string GetAuthPass()
        {
            return ConfigHelper.GetAuthPass();
        }

        private void UpdatePayment(ClientResponse clientResponse, bool isSuccess = false)
        {
            try
            {
                var paymentRepository = new PaymentRepository();
                var payment = paymentRepository.GetById(Payment.PaymentId);
                payment.TransactionId = clientResponse.TransactionId;
                payment.PaymentRelayStatus = (isSuccess) ? PaymentRelayStatuses.SUCCESS
                                                         : PaymentRelayStatuses.FAILED;

                paymentRepository.Update(payment);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"[PaymentRelayModule][UpdatePaymentStatus] FAILED : " +
                                  $"PaymentId: {Payment.PaymentId}" + Environment.NewLine +
                                  $"{ex.Message}");
            }
        }
    }
}