namespace DevFormAz.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class added_formdisslike : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FormDisslikes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FormId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Forms", t => t.FormId, cascadeDelete: false)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.FormId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.FormDisslikes", "UserId", "dbo.Users");
            DropForeignKey("dbo.FormDisslikes", "FormId", "dbo.Forms");
            DropIndex("dbo.FormDisslikes", new[] { "UserId" });
            DropIndex("dbo.FormDisslikes", new[] { "FormId" });
            DropTable("dbo.FormDisslikes");
        }
    }
}
