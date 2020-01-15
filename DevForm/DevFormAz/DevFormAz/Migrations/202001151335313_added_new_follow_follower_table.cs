namespace DevFormAz.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class added_new_follow_follower_table : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Followers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserDetailsId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Follows",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserDetailsId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.UserDetails", "Follower_Id", c => c.Int());
            AddColumn("dbo.UserDetails", "Follow_Id", c => c.Int());
            CreateIndex("dbo.UserDetails", "Follower_Id");
            CreateIndex("dbo.UserDetails", "Follow_Id");
            AddForeignKey("dbo.UserDetails", "Follower_Id", "dbo.Followers", "Id");
            AddForeignKey("dbo.UserDetails", "Follow_Id", "dbo.Follows", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserDetails", "Follow_Id", "dbo.Follows");
            DropForeignKey("dbo.UserDetails", "Follower_Id", "dbo.Followers");
            DropIndex("dbo.UserDetails", new[] { "Follow_Id" });
            DropIndex("dbo.UserDetails", new[] { "Follower_Id" });
            DropColumn("dbo.UserDetails", "Follow_Id");
            DropColumn("dbo.UserDetails", "Follower_Id");
            DropTable("dbo.Follows");
            DropTable("dbo.Followers");
        }
    }
}
