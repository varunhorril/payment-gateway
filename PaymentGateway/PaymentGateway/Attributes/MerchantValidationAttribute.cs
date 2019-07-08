using NLog;
using PaymentGateway.Models.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace PaymentGateway.Attributes
{
    /// <summary>
    /// Checks Basic Auth of request to validate merchant.
    /// </summary>
    public class MerchantValidationAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            var authHeaderValue = actionContext.Request.Headers.Authorization;
            if (!IsAuthHeaderValid(authHeaderValue))
            {
                actionContext.Response.StatusCode = HttpStatusCode.Forbidden;
                actionContext.Response.Content = new StringContent(ApiMessages.AUTH_FAILED);

                return;
            }

            base.OnActionExecuting(actionContext);
        }

        private bool IsAuthHeaderValid(AuthenticationHeaderValue auth)
        {
            try
            {
                var authString = Encoding.UTF8.GetString(Convert.FromBase64String(auth.Parameter));
                var authValues = authString.Split(new char[] { ':' });
                if (string.IsNullOrWhiteSpace(authValues[0]) || string.IsNullOrWhiteSpace(authValues[1]))
                {
                    return false;
                }



            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}