namespace DevFormAz.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class deleted_formdisslike : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.FormDisslikes", "FormId", "dbo.Forms");
            DropForeignKey("dbo.FormDisslikes", "UserId", "dbo.Users");
            DropIndex("dbo.FormDisslikes", new[] { "FormId" });
            DropIndex("dbo.FormDisslikes", new[] { "UserId" });
            DropTable("dbo.FormDisslikes");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.FormDisslikes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FormId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.FormDisslikes", "UserId");
            CreateIndex("dbo.FormDisslikes", "FormId");
            AddForeignKey("dbo.FormDisslikes", "UserId", "dbo.Users", "Id", cascadeDelete: true);
            AddForeignKey("dbo.FormDisslikes", "FormId", "dbo.Forms", "Id", cascadeDelete: true);
        }
    }
}
