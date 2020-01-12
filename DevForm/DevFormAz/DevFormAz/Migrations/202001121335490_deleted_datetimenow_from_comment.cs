namespace DevFormAz.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class deleted_datetimenow_from_comment : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Comments", "UpdateTime");
            DropColumn("dbo.Comments", "LikeCount");
            DropColumn("dbo.Comments", "DisslikeCount");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Comments", "DisslikeCount", c => c.Int(nullable: false));
            AddColumn("dbo.Comments", "LikeCount", c => c.Int(nullable: false));
            AddColumn("dbo.Comments", "UpdateTime", c => c.DateTime(nullable: false));
        }
    }
}
