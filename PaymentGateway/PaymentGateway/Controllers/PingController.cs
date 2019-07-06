using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;

namespace PaymentGateway.Controllers
{
    [Route("")]
    public class PingController : ApiController
    {
        [Route("Ping")]
        [HttpGet]
        public IHttpActionResult Ping()
        {
            return Ok("Service is UP!");
        }
    }
}