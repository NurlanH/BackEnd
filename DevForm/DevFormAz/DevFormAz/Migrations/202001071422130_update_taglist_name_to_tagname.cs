namespace DevFormAz.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update_taglist_name_to_tagname : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TagLists", "TagName", c => c.String());
            DropColumn("dbo.TagLists", "Name");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TagLists", "Name", c => c.String());
            DropColumn("dbo.TagLists", "TagName");
        }
    }
}
