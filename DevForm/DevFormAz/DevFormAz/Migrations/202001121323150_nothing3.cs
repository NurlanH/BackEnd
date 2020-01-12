namespace DevFormAz.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class nothing3 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Forms", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Forms", "Description", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Forms", "Description", c => c.String());
            AlterColumn("dbo.Forms", "Name", c => c.String());
        }
    }
}
