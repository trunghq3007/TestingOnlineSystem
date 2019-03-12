namespace TestingSystem.Data.Migrations
{
    using System.Data.Entity.Migrations;

    /// <summary>
    /// Defines the <see cref="CodeFirst" />
    /// </summary>
    public partial class CodeFirst : DbMigration
    {
        /// <summary>
        /// The Up
        /// </summary>
        public override void Up()
        {
            CreateTable(
                "dbo.Actions",
                c => new
                {
                    ActionId = c.Int(nullable: false, identity: true),
                    ActionName = c.String(nullable: false),
                    Description = c.String(),
                })
                .PrimaryKey(t => t.ActionId);

            CreateTable(
                "dbo.RoleActions",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    RoleId = c.Int(nullable: false),
                    ActionId = c.Int(nullable: false),
                    IsTrue = c.Boolean(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Actions", t => t.ActionId, cascadeDelete: true)
                .ForeignKey("dbo.Roles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.RoleId)
                .Index(t => t.ActionId);

            CreateTable(
                "dbo.Roles",
                c => new
                {
                    RoleId = c.Int(nullable: false, identity: true),
                    RoleName = c.String(nullable: false),
                    Description = c.String(),
                })
                .PrimaryKey(t => t.RoleId);

            CreateTable(
                "dbo.Users",
                c => new
                {
                    UserId = c.Int(nullable: false, identity: true),
                    RoleId = c.Int(nullable: false),
                    UserName = c.String(nullable: false, maxLength: 20),
                    Password = c.String(nullable: false, maxLength: 20),
                    CreatedDate = c.DateTime(),
                    UpdatedDate = c.DateTime(),
                    Status = c.Byte(nullable: false),
                    Name = c.String(nullable: false, maxLength: 50),
                    Phone = c.String(nullable: false, maxLength: 50),
                    Email = c.String(nullable: false, maxLength: 50),
                    Address = c.String(nullable: false, maxLength: 200),
                    Avatar = c.String(nullable: false, maxLength: 200),
                    Note = c.String(),
                })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.Roles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.RoleId)
                .Index(t => t.UserName, unique: true)
                .Index(t => t.Email, unique: true);

            CreateTable(
                "dbo.Candidates",
                c => new
                {
                    CandidatesID = c.Int(nullable: false, identity: true),
                    UserId = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.CandidatesID)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);

            CreateTable(
                "dbo.ExamPapers",
                c => new
                {
                    ExamPaperID = c.Int(nullable: false, identity: true),
                    Title = c.String(nullable: false),
                    Time = c.Int(nullable: false),
                    Status = c.Boolean(nullable: false),
                    NumberOfQuestion = c.Int(nullable: false),
                    IsActive = c.Boolean(nullable: false),
                    CreatedBy = c.Int(nullable: false),
                    CreatedDate = c.DateTime(),
                    ModifiedBy = c.Int(nullable: false),
                    ModifiebDate = c.DateTime(),
                })
                .PrimaryKey(t => t.ExamPaperID)
                .ForeignKey("dbo.Users", t => t.CreatedBy)
                .ForeignKey("dbo.Users", t => t.ModifiedBy)
                .Index(t => t.CreatedBy)
                .Index(t => t.ModifiedBy);

            CreateTable(
                "dbo.ExamPaperQuesions",
                c => new
                {
                    ExamPaperQuesionID = c.Int(nullable: false, identity: true),
                    QuestionID = c.Int(nullable: false),
                    ExamPaperID = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.ExamPaperQuesionID)
                .ForeignKey("dbo.ExamPapers", t => t.ExamPaperID, cascadeDelete: true)
                .ForeignKey("dbo.Questions", t => t.QuestionID, cascadeDelete: true)
                .Index(t => t.QuestionID)
                .Index(t => t.ExamPaperID);

            CreateTable(
                "dbo.Questions",
                c => new
                {
                    QuestionID = c.Int(nullable: false, identity: true),
                    Content = c.String(nullable: false),
                    Image = c.String(),
                    Level = c.Int(nullable: false),
                    CategoryID = c.Int(nullable: false),
                    IsActive = c.Boolean(nullable: false),
                    CreatedBy = c.Int(nullable: false),
                    CreatedDate = c.DateTime(),
                    ModifiedBy = c.Int(nullable: false),
                    ModifiebDate = c.DateTime(),
                })
                .PrimaryKey(t => t.QuestionID)
                .ForeignKey("dbo.Users", t => t.CreatedBy)
                .ForeignKey("dbo.Users", t => t.ModifiedBy)
                .ForeignKey("dbo.QuestionCategories", t => t.CategoryID, cascadeDelete: true)
                .Index(t => t.CategoryID)
                .Index(t => t.CreatedBy)
                .Index(t => t.ModifiedBy);

            CreateTable(
                "dbo.Answers",
                c => new
                {
                    AnswerID = c.Int(nullable: false, identity: true),
                    Content = c.String(nullable: false),
                    IsCorrect = c.Boolean(nullable: false),
                    QuestionID = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.AnswerID)
                .ForeignKey("dbo.Questions", t => t.QuestionID, cascadeDelete: true)
                .Index(t => t.QuestionID);

            CreateTable(
                "dbo.QuestionCategories",
                c => new
                {
                    CategoryID = c.Int(nullable: false, identity: true),
                    Name = c.String(nullable: false),
                    IsActive = c.Boolean(nullable: false),
                    CreatedBy = c.Int(nullable: false),
                    CreatedDate = c.DateTime(),
                    ModifiedBy = c.Int(nullable: false),
                    ModifiebDate = c.DateTime(),
                })
                .PrimaryKey(t => t.CategoryID)
                .ForeignKey("dbo.Users", t => t.CreatedBy)
                .ForeignKey("dbo.Users", t => t.ModifiedBy)
                .Index(t => t.CreatedBy)
                .Index(t => t.ModifiedBy);

            CreateTable(
                "dbo.UserGroups",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    UserId = c.Int(nullable: false),
                    GroupId = c.Int(nullable: false),
                    IsManager = c.Boolean(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Groups", t => t.GroupId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.GroupId);

            CreateTable(
                "dbo.Groups",
                c => new
                {
                    GroupId = c.Int(nullable: false, identity: true),
                    GroupName = c.String(nullable: false),
                    CreatedDate = c.DateTime(nullable: false),
                    UpdatedDate = c.DateTime(nullable: false),
                    Description = c.String(),
                })
                .PrimaryKey(t => t.GroupId);

            CreateTable(
                "dbo.Exams",
                c => new
                {
                    ExamID = c.Int(nullable: false, identity: true),
                    ExamName = c.String(nullable: false),
                    Description = c.String(nullable: false),
                    CreateDate = c.DateTime(nullable: false),
                    StartDate = c.DateTime(nullable: false),
                    EndDate = c.DateTime(nullable: false),
                    Status = c.Byte(nullable: false),
                })
                .PrimaryKey(t => t.ExamID);

            CreateTable(
                "dbo.ManagerTests",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    TestID = c.Int(nullable: false),
                    UserId = c.Int(nullable: false),
                    Type = c.Byte(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Tests", t => t.TestID, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.TestID)
                .Index(t => t.UserId);

            CreateTable(
                "dbo.Tests",
                c => new
                {
                    TestID = c.Int(nullable: false, identity: true),
                    ExamPaperID = c.Int(nullable: false),
                    ExamID = c.Int(nullable: false),
                    TestName = c.String(nullable: false),
                    Description = c.String(nullable: false),
                    CreateDate = c.DateTime(nullable: false),
                    StartDate = c.DateTime(nullable: false),
                    EndDate = c.DateTime(nullable: false),
                    PassingScore = c.String(nullable: false),
                    Status = c.Byte(nullable: false),
                })
                .PrimaryKey(t => t.TestID)
                .ForeignKey("dbo.ExamPapers", t => t.ExamPaperID, cascadeDelete: true)
                .ForeignKey("dbo.Exams", t => t.ExamID, cascadeDelete: true)
                .Index(t => t.ExamPaperID)
                .Index(t => t.ExamID);

            CreateTable(
                "dbo.TestResults",
                c => new
                {
                    TestResultID = c.Int(nullable: false, identity: true),
                    CandidatesID = c.Int(nullable: false),
                    TestID = c.Int(nullable: false),
                    Score = c.Int(nullable: false),
                    Description = c.String(nullable: false),
                })
                .PrimaryKey(t => t.TestResultID)
                .ForeignKey("dbo.Candidates", t => t.CandidatesID, cascadeDelete: true)
                .ForeignKey("dbo.Tests", t => t.TestID, cascadeDelete: true)
                .Index(t => t.CandidatesID)
                .Index(t => t.TestID);
        }

        /// <summary>
        /// The Down
        /// </summary>
        public override void Down()
        {
            DropForeignKey("dbo.TestResults", "TestID", "dbo.Tests");
            DropForeignKey("dbo.TestResults", "CandidatesID", "dbo.Candidates");
            DropForeignKey("dbo.ManagerTests", "UserId", "dbo.Users");
            DropForeignKey("dbo.ManagerTests", "TestID", "dbo.Tests");
            DropForeignKey("dbo.Tests", "ExamID", "dbo.Exams");
            DropForeignKey("dbo.Tests", "ExamPaperID", "dbo.ExamPapers");
            DropForeignKey("dbo.RoleActions", "RoleId", "dbo.Roles");
            DropForeignKey("dbo.UserGroups", "UserId", "dbo.Users");
            DropForeignKey("dbo.UserGroups", "GroupId", "dbo.Groups");
            DropForeignKey("dbo.Users", "RoleId", "dbo.Roles");
            DropForeignKey("dbo.ExamPapers", "ModifiedBy", "dbo.Users");
            DropForeignKey("dbo.Questions", "CategoryID", "dbo.QuestionCategories");
            DropForeignKey("dbo.QuestionCategories", "ModifiedBy", "dbo.Users");
            DropForeignKey("dbo.QuestionCategories", "CreatedBy", "dbo.Users");
            DropForeignKey("dbo.Questions", "ModifiedBy", "dbo.Users");
            DropForeignKey("dbo.ExamPaperQuesions", "QuestionID", "dbo.Questions");
            DropForeignKey("dbo.Questions", "CreatedBy", "dbo.Users");
            DropForeignKey("dbo.Answers", "QuestionID", "dbo.Questions");
            DropForeignKey("dbo.ExamPaperQuesions", "ExamPaperID", "dbo.ExamPapers");
            DropForeignKey("dbo.ExamPapers", "CreatedBy", "dbo.Users");
            DropForeignKey("dbo.Candidates", "UserId", "dbo.Users");
            DropForeignKey("dbo.RoleActions", "ActionId", "dbo.Actions");
            DropIndex("dbo.TestResults", new[] { "TestID" });
            DropIndex("dbo.TestResults", new[] { "CandidatesID" });
            DropIndex("dbo.Tests", new[] { "ExamID" });
            DropIndex("dbo.Tests", new[] { "ExamPaperID" });
            DropIndex("dbo.ManagerTests", new[] { "UserId" });
            DropIndex("dbo.ManagerTests", new[] { "TestID" });
            DropIndex("dbo.UserGroups", new[] { "GroupId" });
            DropIndex("dbo.UserGroups", new[] { "UserId" });
            DropIndex("dbo.QuestionCategories", new[] { "ModifiedBy" });
            DropIndex("dbo.QuestionCategories", new[] { "CreatedBy" });
            DropIndex("dbo.Answers", new[] { "QuestionID" });
            DropIndex("dbo.Questions", new[] { "ModifiedBy" });
            DropIndex("dbo.Questions", new[] { "CreatedBy" });
            DropIndex("dbo.Questions", new[] { "CategoryID" });
            DropIndex("dbo.ExamPaperQuesions", new[] { "ExamPaperID" });
            DropIndex("dbo.ExamPaperQuesions", new[] { "QuestionID" });
            DropIndex("dbo.ExamPapers", new[] { "ModifiedBy" });
            DropIndex("dbo.ExamPapers", new[] { "CreatedBy" });
            DropIndex("dbo.Candidates", new[] { "UserId" });
            DropIndex("dbo.Users", new[] { "Email" });
            DropIndex("dbo.Users", new[] { "UserName" });
            DropIndex("dbo.Users", new[] { "RoleId" });
            DropIndex("dbo.RoleActions", new[] { "ActionId" });
            DropIndex("dbo.RoleActions", new[] { "RoleId" });
            DropTable("dbo.TestResults");
            DropTable("dbo.Tests");
            DropTable("dbo.ManagerTests");
            DropTable("dbo.Exams");
            DropTable("dbo.Groups");
            DropTable("dbo.UserGroups");
            DropTable("dbo.QuestionCategories");
            DropTable("dbo.Answers");
            DropTable("dbo.Questions");
            DropTable("dbo.ExamPaperQuesions");
            DropTable("dbo.ExamPapers");
            DropTable("dbo.Candidates");
            DropTable("dbo.Users");
            DropTable("dbo.Roles");
            DropTable("dbo.RoleActions");
            DropTable("dbo.Actions");
        }
    }
}
