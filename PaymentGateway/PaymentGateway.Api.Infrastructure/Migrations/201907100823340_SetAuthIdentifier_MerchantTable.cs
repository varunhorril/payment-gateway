namespace PaymentGateway.Api.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SetAuthIdentifier_MerchantTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Merchant", "AuthIdentifier", c => c.Int(nullable: false, identity: true));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Merchant", "AuthIdentifier");
        }
    }
}
