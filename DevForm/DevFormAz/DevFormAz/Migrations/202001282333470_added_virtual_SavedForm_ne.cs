namespace DevFormAz.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class added_virtual_SavedForm_ne : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.SavedForms", "FormId", "dbo.Forms");
            DropForeignKey("dbo.SavedForms", "UserDetailId", "dbo.UserDetails");
            DropIndex("dbo.SavedForms", new[] { "FormId" });
            DropIndex("dbo.SavedForms", new[] { "UserDetailId" });
            AddColumn("dbo.Forms", "SavedForm_Id", c => c.Int());
            AddColumn("dbo.UserDetails", "SavedForm_Id", c => c.Int());
            AddColumn("dbo.SavedForms", "UserDetailsId", c => c.Int(nullable: false));
            AddColumn("dbo.SavedForms", "FormsId", c => c.Int(nullable: false));
            CreateIndex("dbo.Forms", "SavedForm_Id");
            CreateIndex("dbo.UserDetails", "SavedForm_Id");
            AddForeignKey("dbo.Forms", "SavedForm_Id", "dbo.SavedForms", "Id");
            AddForeignKey("dbo.UserDetails", "SavedForm_Id", "dbo.SavedForms", "Id");
            DropColumn("dbo.SavedForms", "UserDetailId");
            DropColumn("dbo.SavedForms", "FormId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SavedForms", "FormId", c => c.Int(nullable: false));
            AddColumn("dbo.SavedForms", "UserDetailId", c => c.Int(nullable: false));
            DropForeignKey("dbo.UserDetails", "SavedForm_Id", "dbo.SavedForms");
            DropForeignKey("dbo.Forms", "SavedForm_Id", "dbo.SavedForms");
            DropIndex("dbo.UserDetails", new[] { "SavedForm_Id" });
            DropIndex("dbo.Forms", new[] { "SavedForm_Id" });
            DropColumn("dbo.SavedForms", "FormsId");
            DropColumn("dbo.SavedForms", "UserDetailsId");
            DropColumn("dbo.UserDetails", "SavedForm_Id");
            DropColumn("dbo.Forms", "SavedForm_Id");
            CreateIndex("dbo.SavedForms", "UserDetailId");
            CreateIndex("dbo.SavedForms", "FormId");
            AddForeignKey("dbo.SavedForms", "UserDetailId", "dbo.UserDetails", "Id", cascadeDelete: false);
            AddForeignKey("dbo.SavedForms", "FormId", "dbo.Forms", "Id", cascadeDelete: true);
        }
    }
}
