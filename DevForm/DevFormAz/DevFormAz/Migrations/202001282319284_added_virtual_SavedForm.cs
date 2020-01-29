namespace DevFormAz.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class added_virtual_SavedForm : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Forms", "SavedForm_Id", c => c.Int());
            AddColumn("dbo.UserDetails", "SavedForm_Id", c => c.Int());
            AddColumn("dbo.SavedForms", "UserDetailId", c => c.Int(nullable: false));
            CreateIndex("dbo.Forms", "SavedForm_Id");
            CreateIndex("dbo.UserDetails", "SavedForm_Id");
            AddForeignKey("dbo.Forms", "SavedForm_Id", "dbo.SavedForms", "Id");
            AddForeignKey("dbo.UserDetails", "SavedForm_Id", "dbo.SavedForms", "Id");
            DropColumn("dbo.SavedForms", "UserId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SavedForms", "UserId", c => c.Int(nullable: false));
            DropForeignKey("dbo.UserDetails", "SavedForm_Id", "dbo.SavedForms");
            DropForeignKey("dbo.Forms", "SavedForm_Id", "dbo.SavedForms");
            DropIndex("dbo.UserDetails", new[] { "SavedForm_Id" });
            DropIndex("dbo.Forms", new[] { "SavedForm_Id" });
            DropColumn("dbo.SavedForms", "UserDetailId");
            DropColumn("dbo.UserDetails", "SavedForm_Id");
            DropColumn("dbo.Forms", "SavedForm_Id");
        }
    }
}
