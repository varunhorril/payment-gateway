using NLog;
using PaymentGateway.Api.Business.Interfaces;
using PaymentGateway.Api.Infrastructure.DAL.Repositories;
using PaymentGateway.Models.Constants;
using System;
using PaymentGateway.Core.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PaymentGateway.Modules.Shopper
{
    /// <summary>
    /// Checks if shopper is a new shopper & creates Shopper in DB.
    /// If shopper is a returning shopper, retrieves Shopper from DB.
    /// </summary>
    public class ShopperHandlerModule : IModuleBase
    {
        public string CardId { get; set; }
        public string CardNumber { get; set; }
        public string AmountDue { get; set; }
        public string Currency { get; set; }

        private static Logger _logger = LogManager.GetCurrentClassLogger();
        private ShopperRepository repository = new ShopperRepository();

        public IResponseBase Process()
        {
            var response = new Response();

            try
            {
                var shopper = GetShopper();
                if (shopper != null)
                {
                    response.IsSuccessful = true;
                    response.Data = shopper;
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"[ShopperHandlerModule][Process][FAILED]: {ex.Message}");
            }

            return response;
        }

        private Core.Models.Shopper GetShopper()
        {
            try
            {
                var shopper = repository.GetShopperByCardNumber(CardNumber);
                if (shopper != null)
                {
                    return shopper;
                }

                var newShopperId = Guid.NewGuid();
                shopper = new Core.Models.Shopper()
                {
                    ShopperId = newShopperId,
                    Amount = Convert.ToInt32(AmountDue),
                    CardNumber = CardNumber,
                    Currency = Currency,
                    CardId = Guid.Parse(CardId)
                };

                repository.Insert(shopper);
                return RetrieveNewShopper(newShopperId);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"[ShopperHandlerModule][GetShopper] : {ex.Message}");
                throw;
            }
        }

        private Core.Models.Shopper RetrieveNewShopper(Guid shopperId)
        {
            return repository.GetById(shopperId);
        }
    }
}