namespace DevFormAz.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class n : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Comments", "UserId", "dbo.Users");
            DropIndex("dbo.Comments", new[] { "UserId" });
            AddColumn("dbo.Comments", "UserDetailId", c => c.Int(nullable: false));
            CreateIndex("dbo.Comments", "UserDetailId");
            AddForeignKey("dbo.Comments", "UserDetailId", "dbo.UserDetails", "Id", cascadeDelete: true);
            DropColumn("dbo.Comments", "UserId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Comments", "UserId", c => c.Int(nullable: false));
            DropForeignKey("dbo.Comments", "UserDetailId", "dbo.UserDetails");
            DropIndex("dbo.Comments", new[] { "UserDetailId" });
            DropColumn("dbo.Comments", "UserDetailId");
            CreateIndex("dbo.Comments", "UserId");
            AddForeignKey("dbo.Comments", "UserId", "dbo.Users", "Id", cascadeDelete: true);
        }
    }
}
