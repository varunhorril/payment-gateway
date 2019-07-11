using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymentGateway.Controllers;
using PaymentGateway.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentGateway.Api.Test
{
    [TestClass]
    class StatusCodeTest
    {
        [TestInitialize]
        public void InitializeValidRequestData()
        {
            var paymentRequestModel = new PaymentViewModel()
            {
                MerchantId = "",
                CardNumber = "",
                CardIssuerName = "",
                Currency = "",
                BankName = "",
                CvvNumber = "",
                Amount = 0,
                ExpiryMonthDate = ""
            };
        }

        [TestMethod]
        public void GetRequestOnApi_ShouldFail()
        {

        }

        [TestMethod]
        public void RequestWithNoAuth_ShouldFail()
        {

        }

        [TestMethod]
        public void RequestWithInvalidAuthAndValidRequestBody_ShouldFail()
        {

        }

        [TestMethod]
        public void RequestWithValidAuthAndInvalidRequestBody_ShouldFail()
        {

        }

        [TestMethod]
        public void RequestWithValidAuthAndValidRequestBody_ShouldPass()
        {

        }
    }
}
