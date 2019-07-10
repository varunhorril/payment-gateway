using AcquiringBank.Api.Client.Attributes;
using AcquiringBank.Api.Client.Models;
using AcquiringBank.Api.Client.Models.Constants;
using Newtonsoft.Json;
using NLog;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;

namespace AcquiringBank.Api.Client.Controllers
{
    [Route("")]
    public class ClientController : ApiController
    {
        private static Logger _errorLogger = LogManager.GetLogger("logfile");
        private static Logger _bankLogger = LogManager.GetLogger("transactionfile");
        private string TransactionId;

        [Route("Process")]
        [HttpPost]
        [PaymentGatewayValidation]
        public IHttpActionResult ProcessTransaction(BankProcessViewModel bankModel)
        {
            var clientResponse = new ApiClientResponse();
            try
            {
                if (!ModelState.IsValid)
                {
                    clientResponse.StatusCode = HttpStatusCode.BadRequest;
                    clientResponse.Content = ApiMessages.FAIL;

                    return clientResponse;
                }

                TransactionId = Guid.NewGuid().ToString();
                var processed = IsProcessingComplete(bankModel);
                clientResponse.StatusCode = (processed) ? HttpStatusCode.OK : HttpStatusCode.InternalServerError;
                clientResponse.Content = (processed) ? TransactionId : ApiMessages.SERVER_ERROR;

            }
            catch (Exception ex)
            {
                _errorLogger.Error(ex, $"[ClientController][ProcessTransaction] {ex.Message}");
                clientResponse.StatusCode = HttpStatusCode.InternalServerError;
                clientResponse.Content = ApiMessages.SERVER_ERROR;
            }

            return clientResponse;
        }

        private bool IsProcessingComplete(BankProcessViewModel dataModel)
        {
            try
            {
                var receivedData = JsonConvert.SerializeObject(dataModel);
                _bankLogger.Info(Environment.NewLine + Environment.NewLine
                                + DateTime.UtcNow.ToLongDateString()
                                + Environment.NewLine
                                + $"[PAYMENT RECEIVED][{TransactionId}] "
                                + Environment.NewLine
                                + receivedData);

                return true;
            }
            catch (Exception ex)
            {
                _errorLogger.Error(ex);
            }

            return false;
        }

    }
}