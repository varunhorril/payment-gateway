using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace PaymentGateway.Models.Constants
{
    public class ApiResponse : IHttpActionResult
    {
        public string Content { get; set; }
        public HttpStatusCode StatusCode { get; set; }


        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            var httpResponse = new HttpResponseMessage()
            {
                Content = new StringContent(Content, Encoding.UTF8, "application/json"),
                StatusCode = StatusCode
            };

            return Task.FromResult(httpResponse);
        }
    }
}