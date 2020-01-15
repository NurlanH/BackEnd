namespace DevFormAz.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class deleted_follow_follower : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.UserDetails", "Follower_Id", "dbo.Followers");
            DropForeignKey("dbo.UserDetails", "Follow_Id", "dbo.Follows");
            DropIndex("dbo.UserDetails", new[] { "Follower_Id" });
            DropIndex("dbo.UserDetails", new[] { "Follow_Id" });
            DropColumn("dbo.UserDetails", "Follower_Id");
            DropColumn("dbo.UserDetails", "Follow_Id");
            DropTable("dbo.Followers");
            DropTable("dbo.Follows");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Follows",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserDetailsId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Followers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserDetailsId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.UserDetails", "Follow_Id", c => c.Int());
            AddColumn("dbo.UserDetails", "Follower_Id", c => c.Int());
            CreateIndex("dbo.UserDetails", "Follow_Id");
            CreateIndex("dbo.UserDetails", "Follower_Id");
            AddForeignKey("dbo.UserDetails", "Follow_Id", "dbo.Follows", "Id");
            AddForeignKey("dbo.UserDetails", "Follower_Id", "dbo.Followers", "Id");
        }
    }
}
