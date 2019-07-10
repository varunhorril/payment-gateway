namespace PaymentGateway.Api.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NullableUpdatedOnField : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Bank", "UpdatedOn", c => c.DateTime());
            AlterColumn("dbo.Card", "UpdatedOn", c => c.DateTime());
            AlterColumn("dbo.CardType", "UpdatedOn", c => c.DateTime());
            AlterColumn("dbo.Merchant", "UpdatedOn", c => c.DateTime());
            AlterColumn("dbo.Payment", "UpdatedOn", c => c.DateTime());
            AlterColumn("dbo.Shopper", "UpdatedOn", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Shopper", "UpdatedOn", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Payment", "UpdatedOn", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Merchant", "UpdatedOn", c => c.DateTime(nullable: false));
            AlterColumn("dbo.CardType", "UpdatedOn", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Card", "UpdatedOn", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Bank", "UpdatedOn", c => c.DateTime(nullable: false));
        }
    }
}
