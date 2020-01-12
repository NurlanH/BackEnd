namespace DevFormAz.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class added_from_form_formtime : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Forms", "FormTime", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Forms", "FormTime");
        }
    }
}
