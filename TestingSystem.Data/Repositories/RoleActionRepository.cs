using System;

namespace TestingSystem.Data.Repositories
{
    using TestingSystem.Data.Infrastructure;
    using TestingSystem.Models;

    /// <summary>
    /// Defines the <see cref="IRoleActionRepository" />
    /// </summary>
    public interface IRoleActionRepository : IRepository<RoleAction>
    {
        /// <summary>
        /// The Edit
        /// </summary>
        /// <param name="roleAction">The roleAction<see cref="RoleAction"/></param>
        void Edit(RoleAction roleAction);

        /// <summary>
        /// The Update
        /// </summary>
        /// <param name="list">The list<see cref="string[]"/></param>
        /// <returns>The <see cref="bool"/></returns>
        bool Update(string[] list);
    }

    /// <summary>
    /// Defines the <see cref="RoleActionRepository" />
    /// </summary>
    public class RoleActionRepository : RepositoryBase<RoleAction>, IRoleActionRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RoleActionRepository"/> class.
        /// </summary>
        /// <param name="dbFactory">The dbFactory<see cref="IDbFactory"/></param>
        public RoleActionRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

        /// <summary>
        /// The Edit
        /// </summary>
        /// <param name="roleAction">The roleAction<see cref="RoleAction"/></param>
        public void Edit(RoleAction roleAction)
        {
            base.Update(roleAction);
        }

        /// <summary>
        /// The Update
        /// </summary>
        /// <param name="list">The list<see cref="string[]"/></param>
        /// <returns>The <see cref="bool"/></returns>
        public bool Update(string[] list)
        {
            try
            {
                DbContext.Database.ExecuteSqlCommand("Update RoleActions Set IsTrue='False'");
                foreach (var item in list)
                {
                    int id = int.Parse(item);
                    var entity = DbContext.RoleActions.Find(id);
                    entity.IsTrue = true;
                }
                return DbContext.SaveChanges() != 0;
            }
            catch(Exception e)
            {
                log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                logger.Debug(e.Message);
                return false;
            }
        }
    }
}
