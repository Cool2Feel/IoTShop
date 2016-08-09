namespace IoTShop.Common.Logic.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RenameToQuantity : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "Quantity", c => c.Int(nullable: false));
            DropColumn("dbo.Orders", "Aantal");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Orders", "Aantal", c => c.Int(nullable: false));
            DropColumn("dbo.Orders", "Quantity");
        }
    }
}
