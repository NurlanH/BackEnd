namespace DevFormAz.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class deleted_virtual_subs_from_userdetail : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Subscribes", "UserDetail_Id", "dbo.UserDetails");
            DropIndex("dbo.Subscribes", new[] { "UserDetail_Id" });
            DropColumn("dbo.Subscribes", "UserDetail_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Subscribes", "UserDetail_Id", c => c.Int());
            CreateIndex("dbo.Subscribes", "UserDetail_Id");
            AddForeignKey("dbo.Subscribes", "UserDetail_Id", "dbo.UserDetails", "Id");
        }
    }
}
