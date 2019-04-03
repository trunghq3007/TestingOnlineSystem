namespace TestingSystem.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateturnrow : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TestResults", "Turns", c => c.Int(nullable: false));
            AlterColumn("dbo.Tests", "Description", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Tests", "Description", c => c.String(nullable: false));
            DropColumn("dbo.TestResults", "Turns");
        }
    }
}
