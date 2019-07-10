namespace PaymentGateway.Api.Infrastructure.Migrations
{
    using PaymentGateway.Api.Infrastructure.DAL;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<DAL.PaymentGatewayContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(DAL.PaymentGatewayContext context)
        {
            var seedHelper = new SeedHelper();

            seedHelper.PopulateBanks()
                      .ToList()
                      .ForEach(b => context.Banks.Add(b));

            seedHelper.PopulateCardTypes()
                      .ToList()
                      .ForEach(ct => context.CardTypes.Add(ct));

            seedHelper.PopulateMerchants()
                      .ToList()
                      .ForEach(m => context.Merchants.Add(m));

            seedHelper.PopulateCards()
                      .ToList()
                      .ForEach(c => context.Cards.Add(c));

            base.Seed(context);
        }
    }
}
