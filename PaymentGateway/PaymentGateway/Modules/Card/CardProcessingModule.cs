using NLog;
using PaymentGateway.Api.Business.Interfaces;
using PaymentGateway.Api.Infrastructure.DAL.Repositories;
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
    public class CardProcessingModule : IModuleBase
    {
        public string CardNumber { get; set; }
        public string CardIssuerName { get; set; }

        private int MaxCardNumberLength { get; set; }
        private int MinCardNumberLength { get; set; }
        private int MajorIndustryIdentifier { get; set; }

        private static Logger _logger = LogManager.GetCurrentClassLogger();

        private CardRepository CardRepository = new CardRepository();

        public CardProcessingModule()
        {
            MaxCardNumberLength = ConfigHelper.GetCardNumberMaxLength();
            MinCardNumberLength = ConfigHelper.GetCardNumberMinLength();
        }

        public IResponseBase Process()
        {
            var response = new Response();

            try
            {
                if (IsLengthValid())
                {
                    MajorIndustryIdentifier = Convert.ToInt32(CardNumber.Substring(0, 1));
                    if (IsMIIValid() && HasPassedLuhnValidation())
                    {
                        response.IsSuccessful = true;
                    }

                    var card = GetCard();
                    if (card == null)
                    {
                        response.IsSuccessful = false;
                        response.Message = ApiMessages.INVALID_CARD_ISSUER_NAME;
                    }
                    else
                    {
                        response.Message = ApiMessages.OK;
                        response.Data = card;
                    }

                    return response;
                }

                response.Message = ApiMessages.INVALID_CARD_NUMBER;

                return response;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"[CardValidationModule][Process][FAILED] : {ex.Message}");
                response.Message = ApiMessages.SERVER_ERROR;
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
            return GetValidMIIList().Any(m => m.Equals(MajorIndustryIdentifier));
        }

        private string RemoveCheckSum()
        {
            return CardNumber.Substring(0, CardNumber.Length - 1);
        }

        private List<int> GetValidMIIList()
        {
            return new List<int>()
            {
                4, 5, 6
            };
        }

        #endregion

        private Core.Models.Card GetCard()
        {
            return CardRepository.GetCardByIssuer(CardIssuerName);
        }
    }
}