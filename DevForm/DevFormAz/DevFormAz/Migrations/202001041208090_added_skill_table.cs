namespace DevFormAz.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class added_skill_table : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Skills",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        UserId = c.Int(nullable: false),
                        UserDetail_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.UserDetails", t => t.UserDetail_Id)
                .Index(t => t.UserId)
                .Index(t => t.UserDetail_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Skills", "UserDetail_Id", "dbo.UserDetails");
            DropForeignKey("dbo.Skills", "UserId", "dbo.Users");
            DropIndex("dbo.Skills", new[] { "UserDetail_Id" });
            DropIndex("dbo.Skills", new[] { "UserId" });
            DropTable("dbo.Skills");
        }
    }
}
