namespace IoTShop.Common.Logic.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedDeliverdByOrderLines : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OrderLines", "Delivered", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.OrderLines", "Delivered");
        }
    }
}
