namespace DevFormAz.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class added_subs : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Subscribes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FollowId = c.Int(nullable: false),
                        FollowerId = c.Int(nullable: false),
                        UserDetail_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.UserDetails", t => t.UserDetail_Id)
                .Index(t => t.UserDetail_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Subscribes", "UserDetail_Id", "dbo.UserDetails");
            DropIndex("dbo.Subscribes", new[] { "UserDetail_Id" });
            DropTable("dbo.Subscribes");
        }
    }
}
