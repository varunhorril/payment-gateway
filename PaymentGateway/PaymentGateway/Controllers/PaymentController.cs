using NLog;
using PaymentGateway.Attributes;
using PaymentGateway.Core.Models;
using PaymentGateway.Models.Constants;
using PaymentGateway.Models.ViewModels;
using PaymentGateway.Modules.Bank;
using PaymentGateway.Modules.Card;
using PaymentGateway.Modules.Merchant;
using PaymentGateway.Modules.Payment;
using PaymentGateway.Modules.Shopper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace PaymentGateway.Controllers
{
    [Route("api")]
    public class PaymentController : ApiController
    {
        private static Logger _logger = LogManager.GetCurrentClassLogger();

        [HttpPost]
        [Route("Payment")]
        [MerchantValidation]
        [ValidateModel]
        public IHttpActionResult Pay(PaymentViewModel paymentRequest)
        {
            var apiResponse = new ApiResponse
            {
                Content = ApiMessages.SERVER_ERROR,
                StatusCode = HttpStatusCode.InternalServerError
            };

            try
            {

                var merchantProcess = new GetMerchantModule(paymentRequest.MerchantId).Process();
                if (!merchantProcess.IsSuccessful)
                {
                    apiResponse.StatusCode = HttpStatusCode.BadRequest;
                    apiResponse.Content = ApiMessages.INVALID_MERCHANT_ID;

                    return apiResponse;
                }

                Merchant merchant = (Merchant)merchantProcess.Data;

                var bankProcess = new GetBankModule(paymentRequest.BankName).Process();
                if (!bankProcess.IsSuccessful)
                {
                    apiResponse.StatusCode = HttpStatusCode.BadRequest;
                    apiResponse.Content = ApiMessages.INVALID_BANK_NAME;

                    return apiResponse;
                }

                Bank bank = (Bank)bankProcess.Data;

                var cardProcess = new CardProcessingModule()
                {
                    CardNumber = paymentRequest.CardNumber,
                    CardIssuerName = paymentRequest.CardIssuerName

                }.Process();

                if (!cardProcess.IsSuccessful)
                {
                    apiResponse.StatusCode = HttpStatusCode.BadRequest;
                    apiResponse.Content = cardProcess.Message;

                    return apiResponse;
                }

                Card card = (Card)cardProcess.Data;

                var shopperProcess = new ShopperHandlerModule()
                {
                    CardId = card.CardId.ToString(),
                    CardNumber = paymentRequest.CardNumber,
                    Currency = paymentRequest.Currency,
                    AmountDue = paymentRequest.Amount,
                    CardExpiry = paymentRequest.ExpiryMonthDate,
                    CVVNumber = paymentRequest.CvvNumber

                }.Process();

                if (!shopperProcess.IsSuccessful)
                {
                    return apiResponse;
                }

                Shopper shopper = (Shopper)shopperProcess.Data;

                var paymentProcessing = new PaymentProcessingModule()
                {
                    Bank = bank,
                    Shopper = shopper,
                    Merchant = merchant

                }.Process();

                if (!paymentProcessing.IsSuccessful)
                {
                    return apiResponse;
                }

                Payment payment = (Payment)paymentProcessing.Data;

                var paymentRelay = new PaymentRelayModule()
                {
                    Payment = payment,
                    Merchant = merchant,
                    Shopper = shopper,
                    Card = card

                }.Relay();

                if (paymentRelay.IsSuccessful)
                {
                    apiResponse.Content = payment.PaymentId.ToString();
                    apiResponse.StatusCode = HttpStatusCode.OK;

                    return apiResponse;
                }

            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"[PaymentController][Pay] FAILED : {ex.Message}");

            }

            return apiResponse;
        }

    }
}