using NLog;
using PaymentGateway.Api.Business.Interfaces;
using PaymentGateway.Api.Infrastructure.DAL.Repositories;
using PaymentGateway.Models.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PaymentGateway.Modules.Bank
{
    public class GetBankModule : IModuleBase
    {
        private string BankName { get; set; }
        private static Logger _logger = LogManager.GetCurrentClassLogger();

        public GetBankModule(string bankName)
        {
            BankName = bankName;
        }

        public IResponseBase Process()
        {
            var response = new Response();

            try
            {
                var bank = new BankRepository().GetBankByName(BankName);
                if (bank != null)
                {
                    response.IsSuccessful = true;
                    response.Data = bank;
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"[GetBankModule][Process] FAILED : {ex.Message}");
            }

            return response;
        }
    }
}