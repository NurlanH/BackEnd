namespace DevFormAz.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updated_form_taglist_added_virtual : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TagListForms",
                c => new
                    {
                        TagList_Id = c.Int(nullable: false),
                        Form_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.TagList_Id, t.Form_Id })
                .ForeignKey("dbo.TagLists", t => t.TagList_Id, cascadeDelete: true)
                .ForeignKey("dbo.Forms", t => t.Form_Id, cascadeDelete: true)
                .Index(t => t.TagList_Id)
                .Index(t => t.Form_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TagListForms", "Form_Id", "dbo.Forms");
            DropForeignKey("dbo.TagListForms", "TagList_Id", "dbo.TagLists");
            DropIndex("dbo.TagListForms", new[] { "Form_Id" });
            DropIndex("dbo.TagListForms", new[] { "TagList_Id" });
            DropTable("dbo.TagListForms");
        }
    }
}
