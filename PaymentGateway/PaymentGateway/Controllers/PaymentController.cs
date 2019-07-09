using NLog;
using PaymentGateway.Attributes;
using PaymentGateway.Core.Models;
using PaymentGateway.Models.Constants;
using PaymentGateway.Models.ViewModels;
using PaymentGateway.Modules.Bank;
using PaymentGateway.Modules.Card;
using PaymentGateway.Modules.Merchant;
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
        public IHttpActionResult Pay(PaymentViewModel payment)
        {
            var apiResponse = new ApiResponse();

            try
            {

                var merchantProcess = new GetMerchantModule(payment.MerchantId).Process();
                if (!merchantProcess.IsSuccessful)
                {
                    apiResponse.StatusCode = HttpStatusCode.BadRequest;
                    apiResponse.Content = ApiMessages.INVALID_MERCHANT_ID;

                    return apiResponse;
                }

                Merchant merchant = (Merchant)merchantProcess.Data;

                var bankProcess = new GetBankModule(payment.BankName).Process();
                if (!bankProcess.IsSuccessful)
                {
                    apiResponse.StatusCode = HttpStatusCode.BadRequest;
                    apiResponse.Content = ApiMessages.INVALID_BANK_NAME;

                    return apiResponse;
                }

                Bank bank = (Bank)bankProcess.Data;

                var cardValidation = new CardProcessingModule()
                {
                    CardNumber = payment.CardNumber
                };

                var cardProcess = cardValidation.Process();

                if (!cardProcess.IsSuccessful)
                {
                    apiResponse.StatusCode = HttpStatusCode.BadRequest;
                    apiResponse.Content = cardProcess.Message;

                    return apiResponse;
                }

                Card card = (Card)cardProcess.Data;

                var shopperHandler = new ShopperHandlerModule()
                {
                    CardId = card.CardId.ToString(),
                    CardNumber = payment.CardNumber,
                    Currency = payment.Currency,
                    AmountDue = payment.Amount
                };

                var shopperProcess = shopperHandler.Process();
                if (!shopperProcess.IsSuccessful)
                {
                    apiResponse.StatusCode = HttpStatusCode.InternalServerError;
                    apiResponse.Content = ApiMessages.SERVER_ERROR;

                    return apiResponse;
                }

                Shopper shopper = (Shopper)shopperProcess.Data;

                


            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"[PaymentController][Pay] FAILED : {ex.Message}");

                apiResponse.Content = ApiMessages.SERVER_ERROR;
                apiResponse.StatusCode = HttpStatusCode.InternalServerError;
            }

            return apiResponse;
        }

    }
}