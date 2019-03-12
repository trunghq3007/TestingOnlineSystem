namespace TestingSystem.Data.Infrastructure
{
    /// <summary>
    /// Defines the <see cref="UnitOfWork" />
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        /// <summary>
        /// Defines the dbFactory
        /// </summary>
        private readonly IDbFactory dbFactory;

        /// <summary>
        /// Defines the dbContext
        /// </summary>
        private TestingSystemEntities dbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="UnitOfWork"/> class.
        /// </summary>
        /// <param name="dbFactory">The dbFactory<see cref="IDbFactory"/></param>
        public UnitOfWork(IDbFactory dbFactory)
        {
            this.dbFactory = dbFactory;
        }

        /// <summary>
        /// Gets the DbContext
        /// </summary>
        public TestingSystemEntities DbContext
        {
            get { return dbContext ?? (dbContext = dbFactory.Init()); }
        }

        /// <summary>
        /// The Commit
        /// </summary>
        public void Commit()
        {
            DbContext.Commit();
        }
    }
}
