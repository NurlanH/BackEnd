namespace DevFormAz.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class deleted_mothly_dailyusecount_from_taglist : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.TagLists", "DailyUseCount");
            DropColumn("dbo.TagLists", "MonthlyUseCount");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TagLists", "MonthlyUseCount", c => c.Int(nullable: false));
            AddColumn("dbo.TagLists", "DailyUseCount", c => c.Int(nullable: false));
        }
    }
}
