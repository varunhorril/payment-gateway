using NLog;
using PaymentGateway.Api.Business.Interfaces;
using PaymentGateway.Api.Infrastructure.DAL.Repositories;
using PaymentGateway.Models.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PaymentGateway.Modules.Merchant
{
    public class GetMerchantModule : IModuleBase
    {
        private string MerchantId { get; set; }

        private static Logger _logger = LogManager.GetCurrentClassLogger();
        public GetMerchantModule(string merchantId)
        {
            MerchantId = merchantId;
        }

        public IResponseBase Process()
        {
            var response = new Response();
            try
            {
                var merchant = new MerchantRepository().GetById(Guid.Parse(MerchantId));
                if (merchant != null)
                {
                    response.IsSuccessful = true;
                    response.Data = merchant;
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"[GetMerchantModule][Process] FAILED {ex.Message}");
            }

            return response;
        }
    }
}