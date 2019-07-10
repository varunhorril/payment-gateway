using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Routing;
using HashidsNet;
using Newtonsoft.Json;
using NLog;
using PaymentGateway.Api.Infrastructure.DAL.Repositories;
using PaymentGateway.Models.Constants;

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

        [Route("GetHash")]
        [HttpGet]
        public IHttpActionResult GetHash(string AuthIdentifier, string AuthSalt)
        {
            var apiResponse = new ApiResponse();

            var hashIDs = new Hashids(AuthSalt, 10);
            var hash = hashIDs.Encode(Convert.ToInt32(AuthIdentifier));

            apiResponse.Content = hash;
            apiResponse.StatusCode = HttpStatusCode.OK;

            return apiResponse;
        }

        [Route("Get")]
        [HttpGet]
        public IHttpActionResult GetMerchant()
        {
            var apiResponse = new ApiResponse();

            var repo = new BankRepository();

            var list = repo.GetAll();
            

            apiResponse.Content = JsonConvert.SerializeObject(list);
            apiResponse.StatusCode = HttpStatusCode.OK;

            return apiResponse;
        }
    }
}