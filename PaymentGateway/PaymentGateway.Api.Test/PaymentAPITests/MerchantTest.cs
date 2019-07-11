using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentGateway.Api.Test.Tests
{
    /// <summary>
    /// Contains tests for Merchant Validation
    /// </summary>
    [TestClass]
    class MerchantTest
    {
        [TestMethod]
        public void RequestWithInvalidMerchantId_ShouldFail()
        {

        }

        [TestMethod]
        public void RequestWithInvalidMerchantPassword_ShouldFail()
        {

        }

        [TestMethod]
        public void RequestWithMerchantWithExpiredLicense_ShouldFail()
        {

        }

        [TestMethod]
        public void RequestWithMerchantWithValidLicense_ShouldPass()
        {

        }

        [TestMethod]
        public void RequestWithValidMerchantId_ShouldPass()
        {

        }


    }
}
