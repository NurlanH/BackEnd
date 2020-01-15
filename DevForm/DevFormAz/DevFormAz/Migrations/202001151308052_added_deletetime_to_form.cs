namespace DevFormAz.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class added_deletetime_to_form : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Forms", "isDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.Forms", "WhenIsDeleted", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Forms", "WhenIsDeleted");
            DropColumn("dbo.Forms", "isDeleted");
        }
    }
}
