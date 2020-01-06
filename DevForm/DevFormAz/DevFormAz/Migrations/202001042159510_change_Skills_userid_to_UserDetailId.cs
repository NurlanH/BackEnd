namespace DevFormAz.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class change_Skills_userid_to_UserDetailId : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Skills", "UserDetail_Id", "dbo.UserDetails");
            DropIndex("dbo.Skills", new[] { "UserDetail_Id" });
            RenameColumn(table: "dbo.Skills", name: "UserDetail_Id", newName: "UserDetailId");
            AlterColumn("dbo.Skills", "UserDetailId", c => c.Int(nullable: false));
            CreateIndex("dbo.Skills", "UserDetailId");
            AddForeignKey("dbo.Skills", "UserDetailId", "dbo.UserDetails", "Id", cascadeDelete: true);
            DropColumn("dbo.Skills", "UserId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Skills", "UserId", c => c.Int(nullable: false));
            DropForeignKey("dbo.Skills", "UserDetailId", "dbo.UserDetails");
            DropIndex("dbo.Skills", new[] { "UserDetailId" });
            AlterColumn("dbo.Skills", "UserDetailId", c => c.Int());
            RenameColumn(table: "dbo.Skills", name: "UserDetailId", newName: "UserDetail_Id");
            CreateIndex("dbo.Skills", "UserDetail_Id");
            AddForeignKey("dbo.Skills", "UserDetail_Id", "dbo.UserDetails", "Id");
        }
    }
}
