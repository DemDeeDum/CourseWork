namespace Hotel.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_assessible : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Rooms", "IsAccessible", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Rooms", "IsAccessible");
        }
    }
}
