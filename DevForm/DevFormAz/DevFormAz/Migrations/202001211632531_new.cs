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
                        Name = c.String(nullable: false),
                        Description = c.String(nullable: false),
                        FormTime = c.DateTime(nullable: false),
                        UserDetailId = c.Int(nullable: false),
                        isDeleted = c.Boolean(nullable: false),
                        WhenIsDeleted = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.UserDetails", t => t.UserDetailId, cascadeDelete: true)
                .Index(t => t.UserDetailId);
            
            CreateTable(
                "dbo.FormImages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ImageName = c.String(),
                        FormId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Forms", t => t.FormId, cascadeDelete: true)
                .Index(t => t.FormId);
            
            CreateTable(
                "dbo.FormLikes",
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
                "dbo.FormViewCounts",
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
            
            CreateTable(
                "dbo.TagLists",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TagName = c.String(),
                        Description = c.String(),
                        FormId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
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
                "dbo.Jobs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        JobName = c.String(),
                        InJob = c.DateTime(nullable: false),
                        OutJob = c.DateTime(nullable: false),
                        JobDesc = c.String(),
                        UserDetailId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.UserDetails", t => t.UserDetailId, cascadeDelete: true)
                .Index(t => t.UserDetailId);
            
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
                "dbo.Subscribes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FollowId = c.Int(nullable: false),
                        FollowerId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TagListForms",
                c => new
                    {
                        TagList_Id = c.Int(nullable: false),
                        Form_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.TagList_Id, t.Form_Id })
                .ForeignKey("dbo.TagLists", t => t.TagList_Id, cascadeDelete: false)
                .ForeignKey("dbo.Forms", t => t.Form_Id, cascadeDelete: true)
                .Index(t => t.TagList_Id)
                .Index(t => t.Form_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Comments", "UserId", "dbo.Users");
            DropForeignKey("dbo.Comments", "FormId", "dbo.Forms");
            DropForeignKey("dbo.Forms", "UserDetailId", "dbo.UserDetails");
            DropForeignKey("dbo.UserDetails", "UserId", "dbo.Users");
            DropForeignKey("dbo.Skills", "UserDetailId", "dbo.UserDetails");
            DropForeignKey("dbo.Jobs", "UserDetailId", "dbo.UserDetails");
            DropForeignKey("dbo.TagListForms", "Form_Id", "dbo.Forms");
            DropForeignKey("dbo.TagListForms", "TagList_Id", "dbo.TagLists");
            DropForeignKey("dbo.FormViewCounts", "UserId", "dbo.Users");
            DropForeignKey("dbo.FormViewCounts", "FormId", "dbo.Forms");
            DropForeignKey("dbo.FormLikes", "UserId", "dbo.Users");
            DropForeignKey("dbo.FormLikes", "FormId", "dbo.Forms");
            DropForeignKey("dbo.FormImages", "FormId", "dbo.Forms");
            DropIndex("dbo.Comments", new[] { "UserId" });
            DropIndex("dbo.Comments", new[] { "FormId" });
            DropIndex("dbo.Forms", new[] { "UserDetailId" });
            DropIndex("dbo.UserDetails", new[] { "UserId" });
            DropIndex("dbo.Skills", new[] { "UserDetailId" });
            DropIndex("dbo.Jobs", new[] { "UserDetailId" });
            DropIndex("dbo.TagListForms", new[] { "Form_Id" });
            DropIndex("dbo.TagListForms", new[] { "TagList_Id" });
            DropIndex("dbo.FormViewCounts", new[] { "UserId" });
            DropIndex("dbo.FormViewCounts", new[] { "FormId" });
            DropIndex("dbo.FormLikes", new[] { "UserId" });
            DropIndex("dbo.FormLikes", new[] { "FormId" });
            DropIndex("dbo.FormImages", new[] { "FormId" });
            DropTable("dbo.TagListForms");
            DropTable("dbo.Subscribes");
            DropTable("dbo.Skills");
            DropTable("dbo.Jobs");
            DropTable("dbo.UserDetails");
            DropTable("dbo.TagLists");
            DropTable("dbo.FormViewCounts");
            DropTable("dbo.Users");
            DropTable("dbo.FormLikes");
            DropTable("dbo.FormImages");
            DropTable("dbo.Forms");
            DropTable("dbo.Comments");
        }
    }
}
