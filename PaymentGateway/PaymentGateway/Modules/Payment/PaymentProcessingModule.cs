using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PaymentGateway.Api.Business.Interfaces;
using PaymentGateway.Core.Models;

namespace PaymentGateway.Modules.Payment
{
    public class PaymentProcessingModule : IModuleBase
    {
        public Core.Models.Shopper Shopper { get; set; }
        public Core.Models.Merchant Merchant { get; set; }
        public Core.Models.Bank Bank { get; set; }
        public Core.Models.Card Card { get; set; }

        public IResponseBase Process()
        {
            throw new NotImplementedException();
        }
    }
}