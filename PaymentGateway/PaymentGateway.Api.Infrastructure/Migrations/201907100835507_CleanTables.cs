namespace PaymentGateway.Api.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CleanTables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
               "dbo.Bank",
               c => new
               {
                   BankId = c.Guid(nullable: false),
                   Name = c.String(nullable: false, maxLength: 50),
                   CountryOfRegistration = c.String(),
                   CreatedOn = c.DateTime(nullable: false),
                   UpdatedOn = c.DateTime(nullable: false),
               })
               .PrimaryKey(t => t.BankId);

            CreateTable(
                "dbo.Card",
                c => new
                {
                    CardId = c.Guid(nullable: false),
                    CardTypeId = c.Guid(nullable: false),
                    Name = c.String(nullable: false, maxLength: 100),
                    IssuerName = c.String(nullable: false, maxLength: 100),
                    CreatedOn = c.DateTime(nullable: false),
                    UpdatedOn = c.DateTime(nullable: false),
                })
                .PrimaryKey(t => t.CardId);

            CreateTable(
                "dbo.CardType",
                c => new
                {
                    CardTypeId = c.Guid(nullable: false),
                    CardNumberLength = c.Int(nullable: false),
                    MIILength = c.Int(nullable: false),
                    IINLength = c.Int(nullable: false),
                    ValidationMethod = c.String(),
                    CreatedOn = c.DateTime(nullable: false),
                    UpdatedOn = c.DateTime(nullable: false),
                })
                .PrimaryKey(t => t.CardTypeId);

            CreateTable(
                "dbo.Merchant",
                c => new
                {
                    MerchantId = c.Guid(nullable: false),
                    AuthSalt = c.String(nullable: false, maxLength: 225),
                    Name = c.String(nullable: false, maxLength: 225),
                    CountryOfRegistration = c.String(maxLength: 50),
                    RegistrationExpiry = c.DateTime(nullable: false),
                    CreatedOn = c.DateTime(nullable: false),
                    UpdatedOn = c.DateTime(nullable: false),
                })
                .PrimaryKey(t => t.MerchantId);

            CreateTable(
                "dbo.Payment",
                c => new
                {
                    PaymentId = c.Guid(nullable: false),
                    MerchantId = c.Guid(nullable: false),
                    BankId = c.Guid(nullable: false),
                    ShopperId = c.Guid(nullable: false),
                    TransactionTimeUtc = c.DateTime(nullable: false),
                    TransactionId = c.String(maxLength: 225),
                    PaymentRelayStatus = c.String(maxLength: 25),
                    CreatedOn = c.DateTime(nullable: false),
                    UpdatedOn = c.DateTime(nullable: false),
                })
                .PrimaryKey(t => t.PaymentId);

            CreateTable(
                "dbo.Shopper",
                c => new
                {
                    ShopperId = c.Guid(nullable: false),
                    CardId = c.Guid(nullable: false),
                    CardNumber = c.String(nullable: false, maxLength: 225),
                    Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                    Currency = c.String(nullable: false, maxLength: 25),
                    CVVNumber = c.String(nullable: false, maxLength: 10),
                    CardExpiry = c.String(nullable: false, maxLength: 10),
                    CreatedOn = c.DateTime(nullable: false),
                    UpdatedOn = c.DateTime(nullable: false),
                })
                .PrimaryKey(t => t.ShopperId);



            AlterColumn("dbo.Bank", "UpdatedOn", c => c.DateTime());
            AlterColumn("dbo.Card", "UpdatedOn", c => c.DateTime());
            AlterColumn("dbo.CardType", "UpdatedOn", c => c.DateTime());
            AlterColumn("dbo.Merchant", "UpdatedOn", c => c.DateTime());
            AlterColumn("dbo.Payment", "UpdatedOn", c => c.DateTime());
            AlterColumn("dbo.Shopper", "UpdatedOn", c => c.DateTime());

            AddColumn("dbo.Merchant", "AuthIdentifier", c => c.Int(nullable: false, identity: true));
        }
        
        public override void Down()
        {
        }
    }
}
