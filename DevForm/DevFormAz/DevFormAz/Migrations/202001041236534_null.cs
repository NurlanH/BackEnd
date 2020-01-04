namespace DevFormAz.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _null : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Users", "Skill_Id", "dbo.Skills");
            DropIndex("dbo.Users", new[] { "Skill_Id" });
            DropColumn("dbo.Users", "Skill_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "Skill_Id", c => c.Int());
            CreateIndex("dbo.Users", "Skill_Id");
            AddForeignKey("dbo.Users", "Skill_Id", "dbo.Skills", "Id");
        }
    }
}
