namespace Hotel.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class rebuild_log_fields : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Logs", "BrowserVersion", c => c.String());
            AlterColumn("dbo.Logs", "JavasriptVersion", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Logs", "JavasriptVersion", c => c.Double(nullable: false));
            AlterColumn("dbo.Logs", "BrowserVersion", c => c.Double(nullable: false));
        }
    }
}
