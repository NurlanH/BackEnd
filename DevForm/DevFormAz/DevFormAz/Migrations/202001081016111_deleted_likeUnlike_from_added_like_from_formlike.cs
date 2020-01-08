namespace DevFormAz.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class deleted_likeUnlike_from_added_like_from_formlike : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FormLikes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Count = c.Int(nullable: false),
                        FormId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Forms", t => t.FormId, cascadeDelete: false)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.FormId)
                .Index(t => t.UserId);
            
            DropColumn("dbo.Forms", "LikeCount");
            DropColumn("dbo.Forms", "DisslikeCount");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Forms", "DisslikeCount", c => c.Int(nullable: false));
            AddColumn("dbo.Forms", "LikeCount", c => c.Int(nullable: false));
            DropForeignKey("dbo.FormLikes", "UserId", "dbo.Users");
            DropForeignKey("dbo.FormLikes", "FormId", "dbo.Forms");
            DropIndex("dbo.FormLikes", new[] { "UserId" });
            DropIndex("dbo.FormLikes", new[] { "FormId" });
            DropTable("dbo.FormLikes");
        }
    }
}
