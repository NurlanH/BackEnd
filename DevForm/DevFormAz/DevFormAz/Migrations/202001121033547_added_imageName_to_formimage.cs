namespace DevFormAz.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class added_imageName_to_formimage : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.FormImages", "ImageName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.FormImages", "ImageName");
        }
    }
}
