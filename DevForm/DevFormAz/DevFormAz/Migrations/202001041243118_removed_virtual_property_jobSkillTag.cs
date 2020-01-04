namespace DevFormAz.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removed_virtual_property_jobSkillTag : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Jobs", "UserId", "dbo.Users");
            DropForeignKey("dbo.TagLists", "FormId", "dbo.Forms");
            DropIndex("dbo.Jobs", new[] { "UserId" });
            DropIndex("dbo.TagLists", new[] { "FormId" });
        }
        
        public override void Down()
        {
            CreateIndex("dbo.TagLists", "FormId");
            CreateIndex("dbo.Jobs", "UserId");
            AddForeignKey("dbo.TagLists", "FormId", "dbo.Forms", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Jobs", "UserId", "dbo.Users", "Id", cascadeDelete: true);
        }
    }
}
