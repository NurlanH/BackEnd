namespace DevFormAz.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class added_formimage : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FormImages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FormId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Forms", t => t.FormId, cascadeDelete: true)
                .Index(t => t.FormId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.FormImages", "FormId", "dbo.Forms");
            DropIndex("dbo.FormImages", new[] { "FormId" });
            DropTable("dbo.FormImages");
        }
    }
}
