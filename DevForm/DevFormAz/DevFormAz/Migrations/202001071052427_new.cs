namespace DevFormAz.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _new : DbMigration
    {
        public override void Up()
        {
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
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Email = c.String(nullable: false),
                        FirstName = c.String(nullable: false),
                        Lastname = c.String(nullable: false),
                        Password = c.String(nullable: false, maxLength: 255),
                        UserControlPoint = c.Guid(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Jobs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        JobName = c.String(),
                        InJob = c.DateTime(nullable: false),
                        OutJob = c.DateTime(nullable: false),
                        JobDesc = c.String(),
                        UserId = c.Int(),
                        UserDetail_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.UserDetails", t => t.UserDetail_Id)
                .Index(t => t.UserDetail_Id);
            
            CreateTable(
                "dbo.Skills",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        UserDetailId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.UserDetails", t => t.UserDetailId)
                .Index(t => t.UserDetailId);
            
            CreateTable(
                "dbo.UserDetails",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Specialty = c.String(),
                        Country = c.String(),
                        GithubLink = c.String(),
                        LinkedinLink = c.String(),
                        Biography = c.String(),
                        Image = c.String(),
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
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserDetails", "UserId", "dbo.Users");
            DropForeignKey("dbo.Skills", "UserDetailId", "dbo.UserDetails");
            DropForeignKey("dbo.Jobs", "UserDetail_Id", "dbo.UserDetails");
            DropForeignKey("dbo.Comments", "UserId", "dbo.Users");
            DropForeignKey("dbo.Comments", "FormId", "dbo.Forms");
            DropForeignKey("dbo.Forms", "UserId", "dbo.Users");
            DropIndex("dbo.UserDetails", new[] { "UserId" });
            DropIndex("dbo.Skills", new[] { "UserDetailId" });
            DropIndex("dbo.Jobs", new[] { "UserDetail_Id" });
            DropIndex("dbo.Comments", new[] { "UserId" });
            DropIndex("dbo.Comments", new[] { "FormId" });
            DropIndex("dbo.Forms", new[] { "UserId" });
            DropTable("dbo.TagLists");
            DropTable("dbo.UserDetails");
            DropTable("dbo.Skills");
            DropTable("dbo.Jobs");
            DropTable("dbo.Users");
            DropTable("dbo.Forms");
            DropTable("dbo.Comments");
        }
    }
}
