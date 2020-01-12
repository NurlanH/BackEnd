namespace DevFormAz.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class nothing : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Forms", "FormTime");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Forms", "FormTime", c => c.DateTime(nullable: false));
        }
    }
}
