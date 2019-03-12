namespace TestingSystem.Data.Migrations
{
    using System.Data.Entity.Migrations;

    /// <summary>
    /// Defines the <see cref="updatepassword" />
    /// </summary>
    public partial class updatepassword : DbMigration
    {
        /// <summary>
        /// The Up
        /// </summary>
        public override void Up()
        {
            AlterColumn("dbo.Users", "Password", c => c.String(nullable: false, maxLength: 50));
        }

        /// <summary>
        /// The Down
        /// </summary>
        public override void Down()
        {
            AlterColumn("dbo.Users", "Password", c => c.String(nullable: false, maxLength: 20));
        }
    }
}
