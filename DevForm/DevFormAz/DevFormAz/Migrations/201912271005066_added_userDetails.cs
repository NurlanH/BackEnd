namespace DevFormAz.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class added_userDetails : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserDetails",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Specialty = c.String(),
                        Country = c.String(),
                        GithubLink = c.String(),
                        LinkedinLink = c.String(),
                        Biography = c.String(),
                        Image = c.String(),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserDetails", "UserId", "dbo.Users");
            DropIndex("dbo.UserDetails", new[] { "UserId" });
            DropTable("dbo.UserDetails");
        }
    }
}
