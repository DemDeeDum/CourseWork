namespace Hotel.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_date_to_log : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Logs", "Date", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Logs", "Date");
        }
    }
}
