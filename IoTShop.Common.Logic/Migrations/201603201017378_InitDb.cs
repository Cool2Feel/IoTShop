namespace IoTShop.Common.Logic.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitDb : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Devices",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        RentPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Stock = c.Int(nullable: false),
                        Picture = c.String(),
                        Description = c.String(),
                        Framework_ID = c.Int(),
                        OS_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Frameworks", t => t.Framework_ID)
                .ForeignKey("dbo.OS", t => t.OS_ID)
                .Index(t => t.Framework_ID)
                .Index(t => t.OS_ID);
            
            CreateTable(
                "dbo.Frameworks",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.OS",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.OrderLines",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UserID = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AspNetUsers", t => t.UserID)
                .Index(t => t.UserID);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        DeviceID = c.Int(nullable: false),
                        UserID = c.String(maxLength: 128),
                        Aantal = c.Int(nullable: false),
                        OrderLine_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Devices", t => t.DeviceID, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserID)
                .ForeignKey("dbo.OrderLines", t => t.OrderLine_ID)
                .Index(t => t.DeviceID)
                .Index(t => t.UserID)
                .Index(t => t.OrderLine_ID);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        LastName = c.String(),
                        FirstName = c.String(),
                        Address = c.String(),
                        ZipCode = c.String(),
                        City = c.String(),
                        Country = c.String(),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.DevicesFramework",
                c => new
                    {
                        DeviceId = c.Int(nullable: false),
                        FrameworkId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.DeviceId, t.FrameworkId })
                .ForeignKey("dbo.Devices", t => t.DeviceId, cascadeDelete: true)
                .ForeignKey("dbo.Frameworks", t => t.FrameworkId, cascadeDelete: true)
                .Index(t => t.DeviceId)
                .Index(t => t.FrameworkId);
            
            CreateTable(
                "dbo.DevicesOS",
                c => new
                    {
                        DeviceId = c.Int(nullable: false),
                        OSId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.DeviceId, t.OSId })
                .ForeignKey("dbo.Devices", t => t.DeviceId, cascadeDelete: true)
                .ForeignKey("dbo.OS", t => t.OSId, cascadeDelete: true)
                .Index(t => t.DeviceId)
                .Index(t => t.OSId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.OrderLines", "UserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.Orders", "OrderLine_ID", "dbo.OrderLines");
            DropForeignKey("dbo.Orders", "UserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Orders", "DeviceID", "dbo.Devices");
            DropForeignKey("dbo.DevicesOS", "OSId", "dbo.OS");
            DropForeignKey("dbo.DevicesOS", "DeviceId", "dbo.Devices");
            DropForeignKey("dbo.Devices", "OS_ID", "dbo.OS");
            DropForeignKey("dbo.DevicesFramework", "FrameworkId", "dbo.Frameworks");
            DropForeignKey("dbo.DevicesFramework", "DeviceId", "dbo.Devices");
            DropForeignKey("dbo.Devices", "Framework_ID", "dbo.Frameworks");
            DropIndex("dbo.DevicesOS", new[] { "OSId" });
            DropIndex("dbo.DevicesOS", new[] { "DeviceId" });
            DropIndex("dbo.DevicesFramework", new[] { "FrameworkId" });
            DropIndex("dbo.DevicesFramework", new[] { "DeviceId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Orders", new[] { "OrderLine_ID" });
            DropIndex("dbo.Orders", new[] { "UserID" });
            DropIndex("dbo.Orders", new[] { "DeviceID" });
            DropIndex("dbo.OrderLines", new[] { "UserID" });
            DropIndex("dbo.Devices", new[] { "OS_ID" });
            DropIndex("dbo.Devices", new[] { "Framework_ID" });
            DropTable("dbo.DevicesOS");
            DropTable("dbo.DevicesFramework");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Orders");
            DropTable("dbo.OrderLines");
            DropTable("dbo.OS");
            DropTable("dbo.Frameworks");
            DropTable("dbo.Devices");
        }
    }
}
