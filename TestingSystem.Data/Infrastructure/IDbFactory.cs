namespace TestingSystem.Data.Infrastructure
{
    using System;

    /// <summary>
    /// Defines the <see cref="IDbFactory" />
    /// </summary>
    public interface IDbFactory : IDisposable
    {
        /// <summary>
        /// The Init
        /// </summary>
        /// <returns>The <see cref="TestingSystemEntities"/></returns>
        TestingSystemEntities Init();
    }
}
