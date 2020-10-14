namespace Hotel.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_logs : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BrowserForLogs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        IsMobile = c.Boolean(nullable: false),
                        Platform = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Logs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserName = c.String(),
                        URL = c.String(),
                        BrowserForLogId = c.Int(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.BrowserForLogs", t => t.BrowserForLogId, cascadeDelete: true)
                .Index(t => t.BrowserForLogId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Logs", "BrowserForLogId", "dbo.BrowserForLogs");
            DropIndex("dbo.Logs", new[] { "BrowserForLogId" });
            DropTable("dbo.Logs");
            DropTable("dbo.BrowserForLogs");
        }
    }
}
