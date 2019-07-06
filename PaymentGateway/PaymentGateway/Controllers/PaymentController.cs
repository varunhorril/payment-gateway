using PaymentGateway.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace PaymentGateway.Controllers
{
    [Route("api")]
    public class PaymentController : ApiController
    {
        [HttpPost]
        [Route("Payment")]
        [MerchantValidation]
        public IHttpActionResult Pay()
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