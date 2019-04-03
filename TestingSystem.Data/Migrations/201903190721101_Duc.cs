namespace TestingSystem.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Duc : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ManagerTests", "TestID", "dbo.Tests");
            DropForeignKey("dbo.ManagerTests", "UserId", "dbo.Users");
            DropIndex("dbo.ManagerTests", new[] { "TestID" });
            DropIndex("dbo.ManagerTests", new[] { "UserId" });
            AddColumn("dbo.Exams", "ExamCode", c => c.String(nullable: false));
            AlterColumn("dbo.Exams", "Description", c => c.String());
            DropTable("dbo.ManagerTests");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ManagerTests",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TestID = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                        Type = c.Byte(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AlterColumn("dbo.Exams", "Description", c => c.String(nullable: false));
            DropColumn("dbo.Exams", "ExamCode");
            CreateIndex("dbo.ManagerTests", "UserId");
            CreateIndex("dbo.ManagerTests", "TestID");
            AddForeignKey("dbo.ManagerTests", "UserId", "dbo.Users", "UserId", cascadeDelete: true);
            AddForeignKey("dbo.ManagerTests", "TestID", "dbo.Tests", "TestID", cascadeDelete: true);
        }
    }
}
