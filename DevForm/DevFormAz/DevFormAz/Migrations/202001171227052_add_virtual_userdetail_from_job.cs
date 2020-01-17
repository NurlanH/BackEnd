namespace DevFormAz.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_virtual_userdetail_from_job : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Jobs", "UserDetail_Id", "dbo.UserDetails");
            DropIndex("dbo.Jobs", new[] { "UserDetail_Id" });
            AddColumn("dbo.Jobs", "UserDetailId", c => c.Int(nullable: false));
            CreateIndex("dbo.Jobs", "UserDetailId");
            AddForeignKey("dbo.Jobs", "UserDetailId", "dbo.UserDetails", "Id", cascadeDelete: true);
            DropColumn("dbo.Jobs", "UserId");
            DropColumn("dbo.Jobs", "UserDetail_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Jobs", "UserDetail_Id", c => c.Int());
            AddColumn("dbo.Jobs", "UserId", c => c.Int());
            DropForeignKey("dbo.Jobs", "UserDetailId", "dbo.UserDetails");
            DropIndex("dbo.Jobs", new[] { "UserDetailId" });
            DropColumn("dbo.Jobs", "UserDetailId");
            CreateIndex("dbo.Jobs", "UserDetail_Id");
            AddForeignKey("dbo.Jobs", "UserDetail_Id", "dbo.UserDetails", "Id");
        }
    }
}
