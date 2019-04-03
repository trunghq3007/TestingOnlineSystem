namespace TestingSystem.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ducupdate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ExamPapers", "Code", c => c.String());
            AlterColumn("dbo.Actions", "ActionName", c => c.String());
            DropColumn("dbo.Tests", "StartDate");
            DropColumn("dbo.Tests", "EndDate");
            DropColumn("dbo.Exams", "ExamCode");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Exams", "ExamCode", c => c.String(nullable: false));
            AddColumn("dbo.Tests", "EndDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Tests", "StartDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Actions", "ActionName", c => c.String(nullable: false));
            DropColumn("dbo.ExamPapers", "Code");
        }
    }
}
