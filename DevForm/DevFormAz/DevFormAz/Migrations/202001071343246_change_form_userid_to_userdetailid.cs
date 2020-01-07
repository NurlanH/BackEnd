namespace DevFormAz.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class change_form_userid_to_userdetailid : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Forms", "UserId", "dbo.Users");
            DropIndex("dbo.Forms", new[] { "UserId" });
            AddColumn("dbo.Forms", "UserDetailId", c => c.Int(nullable: false));
            CreateIndex("dbo.Forms", "UserDetailId");
            AddForeignKey("dbo.Forms", "UserDetailId", "dbo.UserDetails", "Id", cascadeDelete: true);
            DropColumn("dbo.Forms", "UserId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Forms", "UserId", c => c.Int(nullable: false));
            DropForeignKey("dbo.Forms", "UserDetailId", "dbo.UserDetails");
            DropIndex("dbo.Forms", new[] { "UserDetailId" });
            DropColumn("dbo.Forms", "UserDetailId");
            CreateIndex("dbo.Forms", "UserId");
            AddForeignKey("dbo.Forms", "UserId", "dbo.Users", "Id", cascadeDelete: true);
        }
    }
}
