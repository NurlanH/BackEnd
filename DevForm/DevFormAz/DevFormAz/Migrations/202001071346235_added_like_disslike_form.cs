namespace DevFormAz.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class added_like_disslike_form : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Forms", "LikeCount", c => c.Int(nullable: false));
            AddColumn("dbo.Forms", "DisslikeCount", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Forms", "DisslikeCount");
            DropColumn("dbo.Forms", "LikeCount");
        }
    }
}
