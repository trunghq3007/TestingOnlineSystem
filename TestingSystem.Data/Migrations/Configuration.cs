namespace TestingSystem.Data.Migrations
{
    using System.Data.Entity.Migrations;

    /// <summary>
    /// Defines the <see cref="Configuration" />
    /// </summary>
    internal sealed class Configuration : DbMigrationsConfiguration<TestingSystem.Data.TestingSystemEntities>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Configuration"/> class.
        /// </summary>
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        /// <summary>
        /// The Seed
        /// </summary>
        /// <param name="context">The context<see cref="TestingSystem.Data.TestingSystemEntities"/></param>
        protected override void Seed(TestingSystem.Data.TestingSystemEntities context)
        {
        }
    }
}
