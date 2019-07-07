using PaymentGateway.Api.Business.Interfaces;
using PaymentGateway.Helpers;
using PaymentGateway.Models.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PaymentGateway.Modules.Card
{
    /// <summary>
    /// Validates card number for length check & against Luhn's Algorithm.
    /// </summary>
    public class CardValidationModule : IModuleBase
    {
        public string CardNumber { get; set; }

        private int MaxCardNumberLength { get; set; }
        private int MinCardNumberLength { get; set; }
        private int MajorIndustryIdentifier { get; set; }

        private List<int> MIIValidNumbers = new List<int>()
        {
            4, 5, 6
        };

        public CardValidationModule()
        {
            MaxCardNumberLength = ConfigHelper.GetCardNumberMaxLength();
            MinCardNumberLength = ConfigHelper.GetCardNumberMinLength();
        }

        public IResponseBase Process()
        {
            var response = new Response()
            {
                IsSuccessful = false
            };

            try
            {
                if (IsLengthValid())
                {
                    MajorIndustryIdentifier = Convert.ToInt32(CardNumber.Substring(0, 1));
                    if (IsMIIValid() && HasPassedLuhnValidation())
                    {
                        response.IsSuccessful = true;
                    }
                }
            }
            catch (Exception ex)
            {

            }

            return response;
        }

        private bool IsLengthValid()
        {
            return (CardNumber.Length == MaxCardNumberLength);
        }

        #region Luhn's Algorithm Validation

        private bool HasPassedLuhnValidation()
        {
            int sum = 0;
            var cardArray = RemoveCheckSum().ToCharArray();
            foreach (var value in cardArray)
            {
                var doubledVal = Convert.ToInt32(value) * 2;
                if (doubledVal > 9)
                {
                    doubledVal -= 9;
                }

                sum += doubledVal;
            }

            return sum % 10 == 0;
        }

        private bool IsMIIValid()
        {
            return MIIValidNumbers.Any(m => m.Equals(MajorIndustryIdentifier));
        }

        private string RemoveCheckSum()
        {
            return CardNumber.Substring(0, CardNumber.Length - 1);
        }

        #endregion
    }
}