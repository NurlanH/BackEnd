namespace DevFormAz.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _new : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Jobs", "UserDetail_Id", "dbo.UserDetails");
            DropIndex("dbo.Jobs", new[] { "UserDetail_Id" });
            CreateTable(
                "dbo.Comments",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Description = c.String(),
                    WritedTime = c.DateTime(nullable: false),
                    UpdateTime = c.DateTime(nullable: false),
                    LikeCount = c.Int(nullable: false),
                    DisslikeCount = c.Int(nullable: false),
                    FormId = c.Int(nullable: false),
                    UserId = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Forms", t => t.FormId, cascadeDelete: false)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.FormId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Forms",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdateTime = c.DateTime(nullable: false),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.TagLists",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        DailyUseCount = c.Int(nullable: false),
                        MonthlyUseCount = c.Int(nullable: false),
                        FormId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Forms", t => t.FormId, cascadeDelete: true)
                .Index(t => t.FormId);
            
            DropColumn("dbo.Jobs", "UserDetail_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Jobs", "UserDetail_Id", c => c.Int());
            DropForeignKey("dbo.TagLists", "FormId", "dbo.Forms");
            DropForeignKey("dbo.Comments", "UserId", "dbo.Users");
            DropForeignKey("dbo.Comments", "FormId", "dbo.Forms");
            DropForeignKey("dbo.Forms", "UserId", "dbo.Users");
            DropIndex("dbo.TagLists", new[] { "FormId" });
            DropIndex("dbo.Comments", new[] { "UserId" });
            DropIndex("dbo.Comments", new[] { "FormId" });
            DropIndex("dbo.Forms", new[] { "UserId" });
            DropTable("dbo.TagLists");
            DropTable("dbo.Forms");
            DropTable("dbo.Comments");
            CreateIndex("dbo.Jobs", "UserDetail_Id");
            AddForeignKey("dbo.Jobs", "UserDetail_Id", "dbo.UserDetails", "Id");
        }
    }
}
