using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace PaymentGateway.Controllers
{
    [Route("api")]
    public class ReportingController
    {
        private static Logger _logger = LogManager.GetCurrentClassLogger();

        [HttpPost]
        [Route("Report")]
        public IHttpActionResult RetrievePayment()
        {

            try
            {



            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"[ReportingController][RetrievePayment] : {ex.Message}");
            }

            return null;
        }
    }
}