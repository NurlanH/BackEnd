namespace DevFormAz.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class added_virtual_SavedForm_without_list : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Forms", "SavedForm_Id", "dbo.SavedForms");
            DropForeignKey("dbo.UserDetails", "SavedForm_Id", "dbo.SavedForms");
            DropIndex("dbo.Forms", new[] { "SavedForm_Id" });
            DropIndex("dbo.UserDetails", new[] { "SavedForm_Id" });
            CreateIndex("dbo.SavedForms", "FormId");
            CreateIndex("dbo.SavedForms", "UserDetailId");
            AddForeignKey("dbo.SavedForms", "FormId", "dbo.Forms", "Id", cascadeDelete: false);
            AddForeignKey("dbo.SavedForms", "UserDetailId", "dbo.UserDetails", "Id", cascadeDelete: true);
            DropColumn("dbo.Forms", "SavedForm_Id");
            DropColumn("dbo.UserDetails", "SavedForm_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.UserDetails", "SavedForm_Id", c => c.Int());
            AddColumn("dbo.Forms", "SavedForm_Id", c => c.Int());
            DropForeignKey("dbo.SavedForms", "UserDetailId", "dbo.UserDetails");
            DropForeignKey("dbo.SavedForms", "FormId", "dbo.Forms");
            DropIndex("dbo.SavedForms", new[] { "UserDetailId" });
            DropIndex("dbo.SavedForms", new[] { "FormId" });
            CreateIndex("dbo.UserDetails", "SavedForm_Id");
            CreateIndex("dbo.Forms", "SavedForm_Id");
            AddForeignKey("dbo.UserDetails", "SavedForm_Id", "dbo.SavedForms", "Id");
            AddForeignKey("dbo.Forms", "SavedForm_Id", "dbo.SavedForms", "Id");
        }
    }
}
