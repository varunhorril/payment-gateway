using PaymentGateway.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentGateway.Api.Infrastructure.DAL
{
    /// <summary>
    /// Helper to populate starter data when seeding DB.
    /// </summary>
    public class SeedHelper
    {
        public static Guid MasterCardTypeId { get; set; }
        public static Guid VisaCardTypeId { get; set; }
        public static Guid AmericanExpressCardTypeId { get; set; }

        public SeedHelper()
        {
            MasterCardTypeId = Guid.NewGuid();
            VisaCardTypeId = Guid.NewGuid();
            AmericanExpressCardTypeId = Guid.NewGuid();
        }

        public static IList<CardType> PopulateCardTypes()
        {
            return new List<CardType>()
            {
                new CardType{ CardTypeId = MasterCardTypeId, CardNumberLength = 16, MIILength = 3, IINLength = 5, ValidationMethod = "LUHN", CreatedOn = DateTime.UtcNow},
                new CardType{ CardTypeId = VisaCardTypeId, CardNumberLength = 16, MIILength = 3, IINLength = 5, ValidationMethod = "LUHN", CreatedOn = DateTime.UtcNow},
                new CardType{ CardTypeId = AmericanExpressCardTypeId, CardNumberLength = 16, MIILength = 3, IINLength = 5, ValidationMethod = "LUHN", CreatedOn = DateTime.UtcNow}
            };
        }

        public static IList<Card> PopulateCards()
        {
            return new List<Card>()
            {
                new Card { CardId = Guid.NewGuid(), CardTypeId = MasterCardTypeId, Name= "World Elite Credit Card", IssuerName="MasterCard", CreatedOn = DateTime.Now  },
                new Card { CardId = Guid.NewGuid(), CardTypeId = VisaCardTypeId, Name= "Visa Gold", IssuerName="Visa", CreatedOn = DateTime.Now  },
                new Card { CardId = Guid.NewGuid(), CardTypeId = VisaCardTypeId, Name= "Visa Classic", IssuerName="Visa", CreatedOn = DateTime.Now  }

            };
        }

        public static IList<Merchant> PopulateMerchants()
        {
            return new List<Merchant>()
            {
                new Merchant { MerchantId = Guid.NewGuid(), Name = "Samsung", CountryOfRegistration= "US", RegistrationExpiry= DateTime.Now.AddYears(2), AuthSalt = Guid.NewGuid().ToString(), CreatedOn = DateTime.UtcNow},
                new Merchant { MerchantId = Guid.NewGuid(), Name = "Adidas", CountryOfRegistration= "US", RegistrationExpiry= DateTime.Now.AddYears(10), AuthSalt = Guid.NewGuid().ToString(), CreatedOn = DateTime.UtcNow},
                new Merchant { MerchantId = Guid.NewGuid(), Name = "Deliveroo", CountryOfRegistration= "UK", RegistrationExpiry= DateTime.Now.AddYears(1), AuthSalt = Guid.NewGuid().ToString(), CreatedOn = DateTime.UtcNow},
                new Merchant { MerchantId = Guid.NewGuid(), Name = "Hopper", CountryOfRegistration= "CH", RegistrationExpiry= DateTime.Now.AddMonths(2), AuthSalt = Guid.NewGuid().ToString(), CreatedOn = DateTime.UtcNow},
                new Merchant { MerchantId = Guid.NewGuid(), Name = "Freedom Pizza", CountryOfRegistration= "US", RegistrationExpiry= DateTime.Now.AddMonths(8), AuthSalt = Guid.NewGuid().ToString(), CreatedOn = DateTime.UtcNow},
            };
        }

        public static IList<Bank> PopulateBanks()
        {
            return new List<Bank>()
            {
                new Bank { BankId = Guid.NewGuid(), CountryOfRegistration = "US", Name = "Morgan Stanley", CreatedOn = DateTime.UtcNow.AddYears(-2)},
                new Bank { BankId = Guid.NewGuid(), CountryOfRegistration = "UK", Name = "Barclays Bank", CreatedOn = DateTime.UtcNow.AddYears(-9)},
                new Bank { BankId = Guid.NewGuid(), CountryOfRegistration = "FR", Name = "BPCE", CreatedOn = DateTime.UtcNow.AddYears(-1)},
                new Bank { BankId = Guid.NewGuid(), CountryOfRegistration = "US", Name = "JPMorgan Chase & Co.", CreatedOn = DateTime.UtcNow.AddMonths(-4)}
            };
        }
    }
}
