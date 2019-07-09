using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
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

        private string _content;
        private HttpStatusCode _statusCode;

        public ApiResponse()
        {
            _content = Content;
            _statusCode = StatusCode;
        }

        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            var httpResponse = new HttpResponseMessage()
            {
                Content = new StringContent(_content),
                StatusCode = _statusCode
            };

            return Task.FromResult(httpResponse);
        }
    }
}