using Newtonsoft.Json;
using NLog;
using PaymentGateway.Api.Business.Interfaces;
using PaymentGateway.Api.Infrastructure.DAL.Repositories;
using PaymentGateway.Core.Exceptions;
using PaymentGateway.Helpers;
using PaymentGateway.Models.Constants;
using PaymentGateway.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PaymentGateway.Modules.Report
{
    public class ReportModule : IModuleBase
    {
        private static Logger _logger = LogManager.GetCurrentClassLogger();

        public string PaymentId { get; set; }

        public IResponseBase Process()
        {
            var response = new Response();
            try
            {
                var payment = RetrievePayment();
                if (payment != null)
                {
                    var shopper = GetShopper(payment);
                    if (shopper != null)
                    {
                        response.Data = BuildReport(payment, shopper);
                        response.IsSuccessful = true;
                    }
                }

            }
            catch (ReportingException repEx)
            {
                _logger.Error(repEx, $"[ReportModule][Process] {repEx.Message}");
            }

            return response;
        }

        private Core.Models.Payment RetrievePayment()
        {
            try
            {
                if (StringUtility.IsValidGuid(PaymentId))
                {
                    var payment = new PaymentRepository().GetById(StringUtility
                                                         .ConvertToGuid(PaymentId));

                    if (payment != null)
                    {
                        return payment;
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"[ReportModule][RetrievePayment] {ex.Message}");

                throw ex;
            }

            return null;
        }
        private Core.Models.Shopper GetShopper(Core.Models.Payment payment)
        {
            try
            {
                return new ShopperRepository().GetById(payment.ShopperId);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"[ReportModule][GetShopper] {ex.Message}");

                throw ex;
            }
        }
        private string BuildReport(Core.Models.Payment payment, Core.Models.Shopper shopper)
        {
            try
            {
                var reportResponse = new ReportResponseViewModel()
                {
                    PaymentId = payment.PaymentId,
                    MaskedCardNumber = StringUtility.GetMaskedCardNumber(shopper.CardNumber),
                    CardExpiry = shopper.CardExpiry,
                    CVVNumber = shopper.CVVNumber,
                    DateProcessed = payment.TransactionTimeUtc.ToLongDateString(),
                    PaymentProcessStatus = payment.PaymentRelayStatus
                };

                return JsonConvert.SerializeObject(reportResponse);
            }
            catch (ReportingException ex)
            {
                _logger.Error(ex, $"[ReportModule][BuildReport] {ex.Message}");

                throw ex;
            }
        }
    }
}