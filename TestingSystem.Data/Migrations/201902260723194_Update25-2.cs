namespace TestingSystem.Data.Migrations
{
    using System.Data.Entity.Migrations;

    /// <summary>
    /// Defines the <see cref="Update252" />
    /// </summary>
    public partial class Update252 : DbMigration
    {
        /// <summary>
        /// The Up
        /// </summary>
        public override void Up()
        {
            AlterColumn("dbo.Users", "Phone", c => c.String(maxLength: 50));
            AlterColumn("dbo.Users", "Address", c => c.String(maxLength: 200));
            AlterColumn("dbo.Users", "Avatar", c => c.String(maxLength: 200));
        }

        /// <summary>
        /// The Down
        /// </summary>
        public override void Down()
        {
            AlterColumn("dbo.Users", "Avatar", c => c.String(nullable: false, maxLength: 200));
            AlterColumn("dbo.Users", "Address", c => c.String(nullable: false, maxLength: 200));
            AlterColumn("dbo.Users", "Phone", c => c.String(nullable: false, maxLength: 50));
        }
    }
}
