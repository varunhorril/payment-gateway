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
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Flurl.Http;
using System.Web;
using System.Net.Security;
using System.IO;

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
        private const string BASIC_AUTH_SCHEME = "Basic";

        public async Task<IResponseBase> RelayAsync()
        {
            IResponseBase response = new Response();

            try
            {
                using (var client = new FlurlClient())
                {
                    var bankResponse = await GetBankApiEndpoint()
                                            .WithBasicAuth(ConfigHelper.GetAuthUser(), GetAuthPass())
                                            .PostStringAsync(GetRequestBody());

                    response = await HandleResponseAsync(bankResponse);
                }
            }
            catch(SocketException sockExp)
            {
                _logger.Error(sockExp, $"[PaymentRelayModule][RelayAsync] NETWORK FAIL: {sockExp.Message}");
            }
            catch (WebException webEx)
            {
                _logger.Error(webEx, $"[PaymentRelayModule][RelayAsync] REQUEST FAILED: {webEx.Message}");
            }
            catch (RelayException ex)
            {
                _logger.Error(ex, $"[PaymentRelayModule][RelayAsync] FAILED: {ex.Message}");
            }

            return response;
        }

        public async Task<IResponseBase> HandleResponseAsync(HttpResponseMessage httpResponse)
        {
            IResponseBase result = new Response();
            var responseString = await httpResponse.Content.ReadAsStringAsync();
            var clientResponse = JsonConvert.DeserializeObject<ClientResponse>(responseString);

            if (!httpResponse.IsSuccessStatusCode)
            {
                UpdatePayment(clientResponse.TransactionId);
                return result;
            }

            UpdatePayment(clientResponse.TransactionId, true);

            return result;
        }

        public IResponseBase Relay()
        {
            IResponseBase response = new Response();
            try
            {
                bool isResponseOK = false;
                var requestBody = Encoding.UTF8.GetBytes(GetRequestBody());
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(GetBankApiEndpoint());

                request.Method = "POST";
                request.ContentLength = requestBody.Length;
                request.ContentType = "application/json";
                request.Headers.Add("Authorization", "Basic " +GetEncodedBasicAuth());

                using (var requestStream = request.GetRequestStream())
                {
                    requestStream.Write(requestBody, 0, requestBody.Length);
                }

                using (var webResponse = (HttpWebResponse) request.GetResponse())
                {
                    using (Stream stream = webResponse.GetResponseStream())
                    {
                        using (StreamReader reader = new StreamReader(stream))
                        {
                            string transactionId = reader.ReadToEnd();

                            isResponseOK = (webResponse.StatusCode == HttpStatusCode.OK) ? true : false;
                            UpdatePayment(transactionId, isResponseOK);

                            response.IsSuccessful = isResponseOK;
                        }
                    }
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

        public AuthenticationHeaderValue SetRequestBasicAuth()
        {
            return new AuthenticationHeaderValue(BASIC_AUTH_SCHEME, GetEncodedBasicAuth());
        }

        private string GetEncodedBasicAuth()
        {
            var username = ConfigHelper.GetAuthUser();
            var password = GetAuthPass();
            var authString = Convert.ToBase64String(Encoding.GetEncoding("ISO-8859-1")
                                                            .GetBytes($"{username}:{password}"));

            return authString;
        }

        public string GetBankApiEndpoint()
        {
            return ConfigHelper.GetApiClientUrl();
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

        private void UpdatePayment(string clientTransactionId, bool isSuccess = false)
        {
            try
            {
                var paymentRepository = new PaymentRepository();
                var payment = paymentRepository.GetById(Payment.PaymentId);
                payment.TransactionId = clientTransactionId;
                payment.PaymentRelayStatus = (isSuccess) ? PaymentRelayStatuses.SUCCESS
                                                         : PaymentRelayStatuses.FAILED;

                paymentRepository.Update(payment);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"[PaymentRelayModule][UpdatePaymentStatus] FAILED : " +
                                  $"PaymentId: {Payment.PaymentId}" + Environment.NewLine +
                                  $"{ex.Message}");

                throw ex;
            }
        }
    }
}