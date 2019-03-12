namespace TestingSystem.Data.Infrastructure
{
    /// <summary>
    /// Defines the <see cref="IUnitOfWork" />
    /// </summary>
    public interface IUnitOfWork
    {
        /// <summary>
        /// The Commit
        /// </summary>
        void Commit();
    }
}
