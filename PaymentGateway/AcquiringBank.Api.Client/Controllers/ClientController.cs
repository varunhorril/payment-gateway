using AcquiringBank.Api.Client.Models;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace AcquiringBank.Api.Client.Controllers
{
    [Route("")]
    public class ClientController : ApiController
    {
        private static Logger _errorLogger = LogManager.GetLogger("logfile");
        private static Logger _bankLogger = LogManager.GetLogger("transactionfile");

        [Route("Process")]
        [HttpPost]
        public IHttpActionResult ProcessTransaction()
        {
            var clientResponse = new ApiClientResponse();
            try
            {


            }
            catch (Exception ex)
            {
                _errorLogger.Error(ex, $"[ClientController][ProcessTransaction] {ex.Message}");
            }

            return clientResponse;
        }
    }
}