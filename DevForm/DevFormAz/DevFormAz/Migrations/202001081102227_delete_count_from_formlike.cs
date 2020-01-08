namespace DevFormAz.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class delete_count_from_formlike : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.FormLikes", "Count");
        }
        
        public override void Down()
        {
            AddColumn("dbo.FormLikes", "Count", c => c.Int(nullable: false));
        }
    }
}
