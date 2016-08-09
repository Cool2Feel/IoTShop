namespace IoTShop.Common.Logic.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedDateByOrderLines : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OrderLines", "Date", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.OrderLines", "Date");
        }
    }
}
