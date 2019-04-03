namespace TestingSystem.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class deleterowscore : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Actions", "ActionName", c => c.String(nullable: false));
            AlterColumn("dbo.Tests", "Status", c => c.Boolean(nullable: false));
            DropColumn("dbo.Tests", "Score");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Tests", "Score", c => c.Int(nullable: false));
            AlterColumn("dbo.Tests", "Status", c => c.Byte(nullable: false));
            AlterColumn("dbo.Actions", "ActionName", c => c.String());
        }
    }
}
