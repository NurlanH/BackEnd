namespace DevFormAz.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class added_virtual_SavedForm_without_list1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Forms", "SavedForm_Id", "dbo.SavedForms");
            DropForeignKey("dbo.UserDetails", "SavedForm_Id", "dbo.SavedForms");
            DropIndex("dbo.Forms", new[] { "SavedForm_Id" });
            DropIndex("dbo.UserDetails", new[] { "SavedForm_Id" });
            AddColumn("dbo.SavedForms", "UserDetailId", c => c.Int(nullable: false));
            AddColumn("dbo.SavedForms", "FormId", c => c.Int(nullable: false));
            CreateIndex("dbo.SavedForms", "FormId");
            CreateIndex("dbo.SavedForms", "UserDetailId");
            AddForeignKey("dbo.SavedForms", "FormId", "dbo.Forms", "Id", cascadeDelete: true);
            AddForeignKey("dbo.SavedForms", "UserDetailId", "dbo.UserDetails", "Id", cascadeDelete: false);
            DropColumn("dbo.Forms", "SavedForm_Id");
            DropColumn("dbo.UserDetails", "SavedForm_Id");
            DropColumn("dbo.SavedForms", "UserDetailsId");
            DropColumn("dbo.SavedForms", "FormsId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SavedForms", "FormsId", c => c.Int(nullable: false));
            AddColumn("dbo.SavedForms", "UserDetailsId", c => c.Int(nullable: false));
            AddColumn("dbo.UserDetails", "SavedForm_Id", c => c.Int());
            AddColumn("dbo.Forms", "SavedForm_Id", c => c.Int());
            DropForeignKey("dbo.SavedForms", "UserDetailId", "dbo.UserDetails");
            DropForeignKey("dbo.SavedForms", "FormId", "dbo.Forms");
            DropIndex("dbo.SavedForms", new[] { "UserDetailId" });
            DropIndex("dbo.SavedForms", new[] { "FormId" });
            DropColumn("dbo.SavedForms", "FormId");
            DropColumn("dbo.SavedForms", "UserDetailId");
            CreateIndex("dbo.UserDetails", "SavedForm_Id");
            CreateIndex("dbo.Forms", "SavedForm_Id");
            AddForeignKey("dbo.UserDetails", "SavedForm_Id", "dbo.SavedForms", "Id");
            AddForeignKey("dbo.Forms", "SavedForm_Id", "dbo.SavedForms", "Id");
        }
    }
}
