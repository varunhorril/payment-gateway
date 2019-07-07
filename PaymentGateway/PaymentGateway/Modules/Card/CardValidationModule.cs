using PaymentGateway.Api.Business.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PaymentGateway.Modules.Card
{
    public class CardValidationModule : IModuleBase
    {
        public string CardNumber { get; set; }

        public IResponseBase Process()
        {
            throw new NotImplementedException();
        }
    }
}