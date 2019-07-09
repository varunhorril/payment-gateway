using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NLog;
using PaymentGateway.Api.Business.Interfaces;
using PaymentGateway.Api.Infrastructure.DAL.Repositories;
using PaymentGateway.Core.Models;
using PaymentGateway.Models.Constants;

namespace PaymentGateway.Modules.Payment
{
    public class PaymentProcessingModule : IModuleBase
    {
        public Core.Models.Shopper Shopper { get; set; }
        public Core.Models.Merchant Merchant { get; set; }
        public Core.Models.Bank Bank { get; set; }

        private static Logger _logger = LogManager.GetCurrentClassLogger();

        private PaymentRepository paymentRepository = new PaymentRepository();

        public IResponseBase Process()
        {
            var response = new Response();

            try
            {
                var paymentId = Guid.NewGuid();
                var payment = new Core.Models.Payment()
                {
                    PaymentId = paymentId,
                    MerchantId = Merchant.MerchantId,
                    BankId = Bank.BankId,
                    ShopperId = Shopper.ShopperId,
                    TransactionId = string.Empty,
                    PaymentRelayStatus = PaymentRelayStatuses.PENDING
                };

                paymentRepository.Insert(payment);

                var storedPayment = GetPayment(paymentId);
                if (storedPayment != null)
                {
                    response.Data = storedPayment;
                    response.IsSuccessful = true;
                }

            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"[PaymentProcessingModule][Process] FAILED: {ex.Message}");
            }

            return response;
        }

        private Core.Models.Payment GetPayment(Guid paymentId)
        {
            try
            {
                var payment = paymentRepository.GetById(paymentId);

                return payment;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"[PaymentProcessingModule][GetPayment] FAILED: {ex.Message}");
            }

            return null;
        }
    }
}