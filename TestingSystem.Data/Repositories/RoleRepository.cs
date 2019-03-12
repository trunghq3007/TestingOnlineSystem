namespace TestingSystem.Data.Repositories
{
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using TestingSystem.Data.Infrastructure;
    using TestingSystem.Models;
    using Action = TestingSystem.Models.Action;

    /// <summary>
    /// Role Repository : Contain functions its communicate to database.
    /// Add : add a role to System.
    /// Edit : Edit a exist role.
    /// Delete : Delete a exist role.
    /// GetById : Get a exist role by roleId.
    /// GetActionInRole : Return Action in Role.
    /// GetAll : Get all roles in system.
    /// </summary>
    public interface IRoleRepository : IRepository<Roles>
    {
        /// <summary>
        /// The Add
        /// </summary>
        /// <param name="role">The role<see cref="Roles"/></param>
        new void Add(Roles role);

        /// <summary>
        /// The Edit
        /// </summary>
        /// <param name="role">The role<see cref="Roles"/></param>
        void Edit(Roles role);

        /// <summary>
        /// The Delete
        /// </summary>
        /// <param name="role">The role<see cref="Roles"/></param>
        new void Delete(Roles role);

        /// <summary>
        /// The GetRoleIdByName
        /// </summary>
        /// <param name="roleName">The roleName<see cref="string"/></param>
        /// <returns>The <see cref="int"/></returns>
        int GetRoleIdByName(string roleName);

        /// <summary>
        /// The GetById
        /// </summary>
        /// <param name="id">The id<see cref="int"/></param>
        /// <returns>The <see cref="Roles"/></returns>
        new Roles GetById(int id);

        /// <summary>
        /// The GetActionInRole
        /// </summary>
        /// <param name="idRole">The idRole<see cref="int"/></param>
        /// <returns>The <see cref="List{Action}"/></returns>
        List<Action> GetActionInRole(int idRole);

        /// <summary>
        /// The GetAll
        /// </summary>
        /// <returns>The <see cref="IEnumerable{Roles}"/></returns>
        new IEnumerable<Roles> GetAll();
    }

    /// <summary>
    /// Defines the <see cref="RoleRepository" />
    /// </summary>
    public class RoleRepository : RepositoryBase<Roles>, IRoleRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RoleRepository"/> class.
        /// </summary>
        /// <param name="dbFactory">The dbFactory<see cref="IDbFactory"/></param>
        public RoleRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

        /// <summary>
        /// The GetRoleIdByName
        /// </summary>
        /// <param name="roleName">The roleName<see cref="string"/></param>
        /// <returns>The <see cref="int"/></returns>
        public int GetRoleIdByName(string roleName)
        {
            int IdRole = 0;
            List<Roles> listRole = base.GetAll() as List<Roles>;
            foreach (var role in listRole)
            {
                if (role.RoleName.Trim() == roleName.Trim())
                    IdRole = role.RoleId;
            }
            return IdRole;
        }

        /// <summary>
        /// The GetById
        /// </summary>
        /// <param name="id">The id<see cref="int"/></param>
        /// <returns>The <see cref="Roles"/></returns>
        public new Roles GetById(int id)
        {
            Roles role = base.GetById(id);
            return role;
        }

        /// <summary>
        /// The GetActionInRole
        /// </summary>
        /// <param name="idRole">The idRole<see cref="int"/></param>
        /// <returns>The <see cref="List{Action}"/></returns>
        public List<Action> GetActionInRole(int idRole)
        {
            List<Action> result = new List<Action>();
            List<int> listIdAction = (from a in DbContext.RoleActions
                                      where (a.RoleId == idRole && a.IsTrue == true)
                                      select a.ActionId).ToList();
            foreach (var item in listIdAction)
            {
                Action action = (from s in DbContext.Actions where s.ActionId == item select s).FirstOrDefault();
                result.Add(action);
            }
            return result;
        }

        /// <summary>
        /// The GetAll
        /// </summary>
        /// <returns>The <see cref="IEnumerable{Roles}"/></returns>
        public override IEnumerable<Roles> GetAll()
        {
            return base.GetAll();
        }

        /// <summary>
        /// The Add
        /// </summary>
        /// <param name="entity">The entity<see cref="Roles"/></param>
        public override void Add(Roles entity)
        {
            base.Add(entity);
            DbContext.SaveChanges();
            int idRole = this.GetRoleIdByName(entity.RoleName);
            List<Action> listActions = (from s in DbContext.Actions select s).ToList();
            foreach (var action in listActions)
            {
                RoleAction roleAction = new RoleAction();
                roleAction.ActionId = action.ActionId;
                roleAction.RoleId = idRole;
                roleAction.IsTrue = false;
                DbContext.RoleActions.Add(roleAction);
                DbContext.SaveChanges();
            }
        }

        /// <summary>
        /// The Edit
        /// </summary>
        /// <param name="role">The role<see cref="Roles"/></param>
        public void Edit(Roles role)
        {
            DbContext.Roles.Attach(role);
            DbContext.Entry(role).State = EntityState.Modified;
            DbContext.SaveChanges();
        }

        /// <summary>
        /// The Delete
        /// </summary>
        /// <param name="entity">The entity<see cref="Roles"/></param>
        public override void Delete(Roles entity)
        {
            List<RoleAction> list = (from s in DbContext.RoleActions select s).ToList();
            int idRole = this.GetRoleIdByName(entity.RoleName);
            foreach (var roleAction in list)
            {
                if (roleAction.RoleId == idRole)
                {
                    DbContext.RoleActions.Remove(roleAction);
                    DbContext.SaveChanges();
                }
            }

            DbContext.Roles.Remove(entity);
            DbContext.SaveChanges();
        }
    }
}
