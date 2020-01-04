namespace DevFormAz.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class added_in_UserDetail_virtual_Jobs : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Jobs", "UserDetail_Id", c => c.Int());
            CreateIndex("dbo.Jobs", "UserDetail_Id");
            AddForeignKey("dbo.Jobs", "UserDetail_Id", "dbo.UserDetails", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Jobs", "UserDetail_Id", "dbo.UserDetails");
            DropIndex("dbo.Jobs", new[] { "UserDetail_Id" });
            DropColumn("dbo.Jobs", "UserDetail_Id");
        }
    }
}
