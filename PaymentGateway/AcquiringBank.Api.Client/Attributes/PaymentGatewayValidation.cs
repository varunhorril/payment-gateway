using NLog;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace AcquiringBank.Api.Client.Attributes
{
    /// <summary>
    /// Checks basic auth of request from Payment Gateway
    /// </summary>
    public class PaymentGatewayValidation : ActionFilterAttribute
    {
        private static Logger _errorLogger = LogManager.GetLogger("logfile");
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            try
            {
                var authHeaderValue = actionContext.Request.Headers.Authorization;
                if (!IsAuthValid(authHeaderValue))
                {
                    actionContext.Response = actionContext.Request.CreateResponse();
                    actionContext.Response.StatusCode = HttpStatusCode.Unauthorized;

                    return;
                }
            }
            catch (Exception ex)
            {
                _errorLogger.Error(ex);
                actionContext.Response = actionContext.Request.CreateResponse();
                actionContext.Response.StatusCode = HttpStatusCode.InternalServerError;

                return;
            }
        }

        private bool IsAuthValid(AuthenticationHeaderValue authentication)
        {
            try
            {
                var authString = Encoding.UTF8.GetString(Convert.FromBase64String(authentication.Parameter));
                var authValues = authString.Split(new char[] { ':' });
                if (string.IsNullOrWhiteSpace(authValues[0]) || string.IsNullOrWhiteSpace(authValues[1]))
                {
                    return false;
                }

                if (authValues[0].Equals(GetUsername(), StringComparison.OrdinalIgnoreCase) 
                    && authValues[1] == GetPass())
                {
                    return true;
                }

            }
            catch (Exception ex)
            {
                _errorLogger.Error(ex);
            }

            return false;
        }

        private string GetUsername()
        {
            var username = string.Empty;
            if (!string.IsNullOrWhiteSpace(ConfigurationManager.AppSettings["ApiRequestUser"]))
            {
                username = ConfigurationManager.AppSettings["ApiRequestUser"];
            }

            return username;
        }

        private string GetPass()
        {
            var pass = string.Empty;
            if (!string.IsNullOrWhiteSpace(ConfigurationManager.AppSettings["ApiRequestPass"]))
            {
                pass = ConfigurationManager.AppSettings["ApiRequestPass"];
            }

            return pass;
        }

    }
}