using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentGateway.Api.Test
{
    /// <summary>
    /// Contains tests for Card Validation
    /// </summary>
    [TestClass]
    class CardTest
    {
        [TestMethod]
        public void CardNumberWithLessThanMinimumCardNumberLength_ShouldFail()
        {

        }

        [TestMethod]
        public void CardNumberWithMoreThanMaximumCardNumberLength_ShouldFail()
        {

        }

        [TestMethod]
        public void CardNumberWithInvalidMIINumber_ShouldFail()
        {

        }

        [TestMethod]
        public void CardNumberWithInvalidCardNumberLength_ShouldFail()
        {

        }

        [TestMethod]
        public void CardNumberWithFailedLuhmAlgorithm_ShouldFail()
        {

        }

        [TestMethod]
        public void CardNumberInvalidCardIssuerName_ShouldFail()
        {

        }

        [TestMethod]
        public void CardExpiryDateWithInvalidMonth_ShouldFail()
        {

        }

        [TestMethod]
        public void CardExpiryDateWithInvalidYear_ShouldFail()
        {

        }

        [TestMethod]
        public void CardExpiryDateWithInvalidMonthAndYear_ShouldFail()
        {

        }

        [TestMethod]
        public void CVVNumberWithLessThanMinimumLength_ShouldFail()
        {

        }

        [TestMethod]
        public void CVVNumberWithMoreThanMaximumLength_ShouldFail()
        {

        }

        [TestMethod]
        public void CardNumberWithValidLuhnAlgorithm_ShouldPass()
        {

        }

        [TestMethod]
        public void CVVNumberWithCorrectLength_ShouldPass()
        {

        }

        [TestMethod]
        public void CVVNumberWithValidMIINumber_ShouldPass()
        {

        }

        [TestMethod]
        public void CardExpiryWithValidMonthAndYear_ShouldPass()
        {

        }

    }
}
