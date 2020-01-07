namespace DevFormAz.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class deleted_updatedate_form : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Forms", "UpdateTime");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Forms", "UpdateTime", c => c.DateTime(nullable: false));
        }
    }
}
