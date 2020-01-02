namespace DevFormAz.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Added_User_UserControl_Prop : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "UserControlPoint", c => c.Guid(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "UserControlPoint");
        }
    }
}
