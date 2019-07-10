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
                        var card = GetCard();
                        if (card == null)
                        {
                            response.Message = ApiMessages.INVALID_CARD_ISSUER_NAME;
                        }
                        else
                        {
                            response.IsSuccessful = true;
                            response.Message = ApiMessages.OK;
                            response.Data = card;
                        }

                        return response;
                    }
                }

                response.Message = ApiMessages.INVALID_CARD_NUMBER;
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
            try
            {
                int sum = 0;
                var cardArray = CardNumber.ToArray().ToList();
                var valuesToDouble = new List<string>();
                var valuesNotDoubled = new List<string>();

                for (int i = 0; i < cardArray.Count(); i++)
                {
                    if (i % 2 == 0)
                    {
                        valuesToDouble.Add(cardArray[i].ToString());
                    }
                    else
                    {
                        valuesNotDoubled.Add(cardArray[i].ToString());
                    }
                }


                foreach (var value in valuesToDouble)
                {
                    var doubledVal = Convert.ToInt32(value) * 2;
                    if (doubledVal > 9)
                    {
                        doubledVal -= 9;
                    }

                    sum += doubledVal;
                }

                foreach (var value in valuesNotDoubled)
                {
                    sum += Convert.ToInt32(value);
                }

                return sum % 10 == 0;

            }
            catch (IndexOutOfRangeException indexExp)
            {
                _logger.Warn(indexExp, $"[CardProcessingModule][Luhn Validation] Index out of bounds : {indexExp.Message}");
            }
            catch (Exception ex)
            {
                _logger.Warn(ex, $"[CardProcessingModule][Luhn Validation] Index out of bounds : {ex.Message}");
            }

            return false;
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