using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Net.Http;
using System.Net;
using PaymentGateway.Models.Constants;

namespace PaymentGateway.Attributes
{
    public class ValidateModelAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            if (!actionContext.ModelState.IsValid)
            {
                actionContext.Response = actionContext.Request
                                                      .CreateErrorResponse
                                                      (HttpStatusCode.BadRequest, ApiMessages.FAILED);

                #region Local output : Return ModelState errors dictionary
                //actionContext.Response = actionContext.Request
                //                                      .CreateErrorResponse
                //                                      (HttpStatusCode.BadRequest, actionContext.ModelState); 
                #endregion
            }
        }
    }
}