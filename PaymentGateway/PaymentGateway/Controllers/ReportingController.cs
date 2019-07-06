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
        [HttpPost]
        [Route("Report")]
        public IHttpActionResult RetrievePayment()
        {
            try
            {



            }
            catch (Exception ex)
            {

            }

            return null;
        }
    }
}