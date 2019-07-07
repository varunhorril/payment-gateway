using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;
using NLog;

namespace PaymentGateway.Controllers
{
    [Route("")]
    public class PingController : ApiController
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        [Route("Ping")]
        [HttpGet]
        public IHttpActionResult Ping()
        {

            return Ok("Service is UP!");
        }
    }
}