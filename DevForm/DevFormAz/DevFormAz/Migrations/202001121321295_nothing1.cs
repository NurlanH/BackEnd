namespace DevFormAz.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class nothing1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Forms", "FormTime", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Forms", "FormTime");
        }
    }
}
