namespace Hotel.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class rebuild_logs : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Logs", "BrowserForLogId", "dbo.BrowserForLogs");
            DropIndex("dbo.Logs", new[] { "BrowserForLogId" });
            AddColumn("dbo.Logs", "IsMobile", c => c.Boolean(nullable: false));
            AddColumn("dbo.Logs", "Platform", c => c.String());
            AddColumn("dbo.Logs", "BrowserName", c => c.String());
            AddColumn("dbo.Logs", "BrowserVersion", c => c.Double(nullable: false));
            AddColumn("dbo.Logs", "JavasriptVersion", c => c.Double(nullable: false));
            AddColumn("dbo.Logs", "Exeption", c => c.String());
            DropColumn("dbo.Logs", "BrowserForLogId");
            DropTable("dbo.BrowserForLogs");
        }
        
        public override void Down()
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
            
            AddColumn("dbo.Logs", "BrowserForLogId", c => c.Int(nullable: false));
            DropColumn("dbo.Logs", "Exeption");
            DropColumn("dbo.Logs", "JavasriptVersion");
            DropColumn("dbo.Logs", "BrowserVersion");
            DropColumn("dbo.Logs", "BrowserName");
            DropColumn("dbo.Logs", "Platform");
            DropColumn("dbo.Logs", "IsMobile");
            CreateIndex("dbo.Logs", "BrowserForLogId");
            AddForeignKey("dbo.Logs", "BrowserForLogId", "dbo.BrowserForLogs", "Id", cascadeDelete: true);
        }
    }
}
