namespace TestingSystem.Data.Migrations
{
    using System.Data.Entity.Migrations;

    /// <summary>
    /// Defines the <see cref="Modified13team1v10" />
    /// </summary>
    public partial class Modified13team1v10 : DbMigration
    {
        /// <summary>
        /// The Up
        /// </summary>
        public override void Up()
        {
            AddColumn("dbo.Answers", "AnswerContent", c => c.String(nullable: false));
            DropColumn("dbo.Answers", "Content");
        }

        /// <summary>
        /// The Down
        /// </summary>
        public override void Down()
        {
            AddColumn("dbo.Answers", "Content", c => c.String(nullable: false));
            DropColumn("dbo.Answers", "AnswerContent");
        }
    }
}
