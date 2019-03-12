namespace TestingSystem.Data.Configuration
{
    using System.Data.Entity.ModelConfiguration;
    using TestingSystem.Models;

    /// <summary>
    /// Defines the <see cref="UserConfigtion" />
    /// </summary>
    internal class UserConfigtion : EntityTypeConfiguration<User>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserConfigtion"/> class.
        /// </summary>
        public UserConfigtion()
        {
        }
    }
}
