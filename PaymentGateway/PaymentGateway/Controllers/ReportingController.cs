using NLog;
using PaymentGateway.Attributes;
using PaymentGateway.Models.Constants;
using PaymentGateway.Models.ViewModels;
using PaymentGateway.Modules.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;

namespace PaymentGateway.Controllers
{
    [Route("api")]
    public class ReportingController
    {
        private static Logger _logger = LogManager.GetCurrentClassLogger();

        [HttpPost]
        [Route("Report")]
        [MerchantValidation]
        public IHttpActionResult RetrievePayment(ReportRequestViewModel report)
        {
            var apiResponse = new ApiResponse();

            try
            {
                var reportProcessModule = new ReportModule()
                {
                    PaymentId = report.PaymentId

                }.Process();

                if (reportProcessModule.IsSuccessful)
                {
                    apiResponse.Content = (string)reportProcessModule.Data;
                    apiResponse.StatusCode = HttpStatusCode.OK;

                    return apiResponse;
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"[ReportingController][RetrievePayment] : {ex.Message}");
            }

            apiResponse.StatusCode = HttpStatusCode.InternalServerError;

            return apiResponse;
        }
    }
}