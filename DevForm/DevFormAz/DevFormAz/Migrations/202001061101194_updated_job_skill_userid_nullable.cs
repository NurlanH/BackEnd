namespace DevFormAz.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updated_job_skill_userid_nullable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Skills", "UserDetailId", "dbo.UserDetails");
            DropIndex("dbo.Skills", new[] { "UserDetailId" });
            AlterColumn("dbo.Jobs", "UserId", c => c.Int());
            AlterColumn("dbo.Skills", "UserDetailId", c => c.Int());
            CreateIndex("dbo.Skills", "UserDetailId");
            AddForeignKey("dbo.Skills", "UserDetailId", "dbo.UserDetails", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Skills", "UserDetailId", "dbo.UserDetails");
            DropIndex("dbo.Skills", new[] { "UserDetailId" });
            AlterColumn("dbo.Skills", "UserDetailId", c => c.Int(nullable: false));
            AlterColumn("dbo.Jobs", "UserId", c => c.Int(nullable: false));
            CreateIndex("dbo.Skills", "UserDetailId");
            AddForeignKey("dbo.Skills", "UserDetailId", "dbo.UserDetails", "Id", cascadeDelete: true);
        }
    }
}
