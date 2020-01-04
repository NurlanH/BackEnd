namespace DevFormAz.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changed_virtual_user_to_list_user : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Skills", "UserId", "dbo.Users");
            DropIndex("dbo.Skills", new[] { "UserId" });
            AddColumn("dbo.Users", "Skill_Id", c => c.Int());
            CreateIndex("dbo.Users", "Skill_Id");
            AddForeignKey("dbo.Users", "Skill_Id", "dbo.Skills", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Users", "Skill_Id", "dbo.Skills");
            DropIndex("dbo.Users", new[] { "Skill_Id" });
            DropColumn("dbo.Users", "Skill_Id");
            CreateIndex("dbo.Skills", "UserId");
            AddForeignKey("dbo.Skills", "UserId", "dbo.Users", "Id", cascadeDelete: true);
        }
    }
}
