namespace TestingSystem.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class duc_update : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ExamPapers", "ExamPaperCode", c => c.String());
            DropColumn("dbo.ExamPapers", "Code");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ExamPapers", "Code", c => c.String());
            DropColumn("dbo.ExamPapers", "ExamPaperCode");
        }
    }
}
