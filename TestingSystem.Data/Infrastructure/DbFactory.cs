namespace TestingSystem.Data.Infrastructure
{
    /// <summary>
    /// Defines the <see cref="DbFactory" />
    /// </summary>
    public class DbFactory : Disposable, IDbFactory
    {
        /// <summary>
        /// Defines the dbContext
        /// </summary>
        internal TestingSystemEntities dbContext;

        /// <summary>
        /// The Init
        /// </summary>
        /// <returns>The <see cref="TestingSystemEntities"/></returns>
        public TestingSystemEntities Init()
        {
            return dbContext ?? (dbContext = new TestingSystemEntities());
        }

        /// <summary>
        /// The DisposeCore
        /// </summary>
        protected override void DisposeCore()
        {
            if (dbContext != null)
                dbContext.Dispose();
        }
    }
}
