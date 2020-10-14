namespace Hotel.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_unlucky_requests : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UnluckyRequests",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RoomClassName = c.String(),
                        PeopleCount = c.Byte(nullable: false),
                        StartDate = c.DateTime(nullable: false),
                        FinishDate = c.DateTime(nullable: false),
                        ApplicationUserId = c.String(maxLength: 128),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUserId)
                .Index(t => t.ApplicationUserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UnluckyRequests", "ApplicationUserId", "dbo.AspNetUsers");
            DropIndex("dbo.UnluckyRequests", new[] { "ApplicationUserId" });
            DropTable("dbo.UnluckyRequests");
        }
    }
}
