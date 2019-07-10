using NLog;
using PaymentGateway.Models.Constants;
using PaymentGateway.Modules.Merchant;
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
        private static Logger _logger = LogManager.GetCurrentClassLogger();

        public override void OnActionExecuting(HttpActionContext actionContext)
        {

            try
            {
                var authHeaderValue = actionContext.Request.Headers.Authorization;
                if (!IsAuthHeaderValid(authHeaderValue))
                {
                    actionContext.Response = actionContext.Request.CreateResponse();

                    actionContext.Response.StatusCode = HttpStatusCode.Unauthorized;
                    actionContext.Response.Content = new StringContent(ApiMessages.AUTH_FAILED, Encoding.UTF8, "application/json");

                    return;
                }
            }
            catch (Exception ex)
            {
                _logger.Warn(ex, $"[MerchantValidationAttribute][OnActionExecuting] : {ex.Message}");

                actionContext.Response = actionContext.Request.CreateResponse();
                actionContext.Response.StatusCode = HttpStatusCode.InternalServerError;
                actionContext.Response.Content = new StringContent(ApiMessages.SERVER_ERROR, Encoding.UTF8, "application/json");

                return;
            }
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

                var merchantValidation = new MerchantAuthValidationModule()
                {
                    MerchantUserId = authValues[0],
                    HashId = authValues[1]
                };

                return merchantValidation.Process().IsSuccessful;

            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"[MerchantValidationAttribute][IsAuthHeaderValid][FAILED] {ex.Message}");

                return false;
            }
        }
    }
}