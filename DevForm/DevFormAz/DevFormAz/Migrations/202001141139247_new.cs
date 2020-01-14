namespace DevFormAz.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _new : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Forms", "FormTime", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Forms", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Forms", "Description", c => c.String(nullable: false));
            DropColumn("dbo.Comments", "UpdateTime");
            DropColumn("dbo.Comments", "LikeCount");
            DropColumn("dbo.Comments", "DisslikeCount");
            DropColumn("dbo.Forms", "CreatedDate");
            DropColumn("dbo.TagLists", "DailyUseCount");
            DropColumn("dbo.TagLists", "MonthlyUseCount");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TagLists", "MonthlyUseCount", c => c.Int(nullable: false));
            AddColumn("dbo.TagLists", "DailyUseCount", c => c.Int(nullable: false));
            AddColumn("dbo.Forms", "CreatedDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Comments", "DisslikeCount", c => c.Int(nullable: false));
            AddColumn("dbo.Comments", "LikeCount", c => c.Int(nullable: false));
            AddColumn("dbo.Comments", "UpdateTime", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Forms", "Description", c => c.String());
            AlterColumn("dbo.Forms", "Name", c => c.String());
            DropColumn("dbo.Forms", "FormTime");
        }
    }
}
