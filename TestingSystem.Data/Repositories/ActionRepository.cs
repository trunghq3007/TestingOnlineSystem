namespace TestingSystem.Data.Repositories
{
    using log4net;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using TestingSystem.Data.Infrastructure;
    using TestingSystem.Models;
    using Action = TestingSystem.Models.Action;

    /// <summary>
    /// Defines the <see cref="IActionRepository" />
    /// </summary>
    public interface IActionRepository : IRepository<Action>
    {
        /// <summary>
        /// The GetActionIdByName
        /// </summary>
        /// <param name="actionName">The actionName<see cref="string"/></param>
        /// <returns>The <see cref="int"/></returns>
        int GetActionIdByName(string actionName);

        /// <summary>
        /// The Add
        /// </summary>
        /// <param name="action">The action<see cref="Action"/></param>
        new void Add(Action action);

        /// <summary>
        /// The Delete
        /// </summary>
        /// <param name="action">The action<see cref="Action"/></param>
        new void Delete(Action action);

        /// <summary>
        /// The AddActionToRole
        /// </summary>
        /// <param name="idAction">The idAction<see cref="int"/></param>
        /// <param name="idRole">The idRole<see cref="int"/></param>
        void AddActionToRole(int idAction, int idRole);

        /// <summary>
        /// The GetAll
        /// </summary>
        /// <returns>The <see cref="IEnumerable{Action}"/></returns>
        new IEnumerable<Action> GetAll();

        /// <summary>
        /// The Delete
        /// </summary>
        /// <param name="id">The id<see cref="int"/></param>
        void Delete(int id);

        /// <summary>
        /// The Get
        /// </summary>
        /// <param name="id">The id<see cref="int"/></param>
        /// <returns>The <see cref="Action"/></returns>
        Action Get(int id);

        /// <summary>
        /// The GetRoleses
        /// </summary>
        /// <param name="id">The id<see cref="int"/></param>
        /// <returns>The <see cref="List{Roles}"/></returns>
        List<Roles> GetRoleses(int id);

        /// <summary>
        /// The Search
        /// </summary>
        /// <param name="key">The key<see cref="string"/></param>
        /// <returns>The <see cref="IEnumerable{Action}"/></returns>
        IEnumerable<Action> Search(string key);

        /// <summary>
        /// The ScanUpdate
        /// </summary>
        /// <param name="ActionNames">The ActionNames<see cref="List{string}"/></param>
        /// <returns>The <see cref="bool"/></returns>
        bool ScanUpdate(List<string> ActionNames);

        /// <summary>
        /// The Update
        /// </summary>
        /// <param name="entity">The entity<see cref="Action"/></param>
        /// <returns>The <see cref="bool"/></returns>
        bool Update(Action entity);
    }

    /// <summary>
    /// Defines the <see cref="ActionRepository" />
    /// </summary>
    public class ActionRepository : RepositoryBase<Action>, IActionRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ActionRepository"/> class.
        /// </summary>
        /// <param name="dbFactory">The dbFactory<see cref="IDbFactory"/></param>
        public ActionRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

        /// <summary>
        /// The GetActionIdByName
        /// </summary>
        /// <param name="actionName">The actionName<see cref="string"/></param>
        /// <returns>The <see cref="int"/></returns>
        public int GetActionIdByName(string actionName)
        {
            try
            {
                int IdAction = 0;
                if (base.GetAll() is List<Action> listActions)
                    foreach (var action in listActions)
                    {
                        if (actionName.Trim() == action.ActionName.Trim())
                        {
                            IdAction = action.ActionId;
                        }
                    }

                return IdAction;
            }
            catch (Exception e)
            {
                ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                logger.Debug(e.Message);
                return 0;
            }
        }

        /// <summary>
        /// The Add
        /// </summary>
        /// <param name="entity">The entity<see cref="Action"/></param>
        public override void Add(Action entity)
        {
            try
            {
                base.Add(entity);
                DbContext.SaveChanges();
                int IdAction = this.GetActionIdByName(entity.ActionName);
                List<Roles> listRoles = (from s in DbContext.Roles select s).ToList();
                foreach (var role in listRoles)
                {
                    RoleAction roleAction = new RoleAction();
                    roleAction.ActionId = IdAction;
                    roleAction.RoleId = role.RoleId;
                    roleAction.IsTrue = role.RoleId == 1;
                    DbContext.RoleActions.Add(roleAction);
                    DbContext.SaveChanges();
                }
            }
            catch (Exception e)
            {
                ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                logger.Debug(e.Message);
            }
        }

        /// <summary>
        /// The GetAll
        /// </summary>
        /// <returns>The <see cref="IEnumerable{Action}"/></returns>
        public override IEnumerable<Action> GetAll()
        {
            return base.GetAll();
        }

        /// <summary>
        /// The Delete
        /// </summary>
        /// <param name="id">The id<see cref="int"/></param>
        public void Delete(int id)
        {
            try
            {

                Action entity = DbContext.Actions.Find(id);
                List<RoleAction> list = (from s in DbContext.RoleActions where s.ActionId == entity.ActionId select s).ToList();
                foreach (RoleAction roleAction in list)
                    DbContext.RoleActions.Remove(roleAction);
                DbContext.SaveChanges();
                base.Delete(entity);
                DbContext.SaveChanges();
            }
            catch (Exception e)
            {
                ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                logger.Debug(e.Message);
            }
        }

        /// <summary>
        /// The AddActionToRole
        /// </summary>
        /// <param name="idAction">The idAction<see cref="int"/></param>
        /// <param name="idRole">The idRole<see cref="int"/></param>
        public void AddActionToRole(int idAction, int idRole)
        {
            try
            {
                RoleAction roleAction =
                    (from s in DbContext.RoleActions where (s.ActionId == idAction && s.RoleId == idRole) select s)
                    .FirstOrDefault();
                roleAction.IsTrue = true;
                DbContext.RoleActions.Attach(roleAction);
                DbContext.Entry(roleAction).State = EntityState.Modified;
                DbContext.SaveChanges();
            }
            catch (Exception e)
            {
                ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                logger.Debug(e.Message);
            }
        }

        /// <summary>
        /// The Get
        /// </summary>
        /// <param name="id">The id<see cref="int"/></param>
        /// <returns>The <see cref="Action"/></returns>
        public Action Get(int id)
        {
            try
            {
                return (from s in DbContext.Actions where s.ActionId == id select s).FirstOrDefault();
            }
            catch (Exception e)
            {
                ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                logger.Debug(e.Message);
                return null;
            }
        }

        /// <summary>
        /// The GetRoleses
        /// </summary>
        /// <param name="id">The id<see cref="int"/></param>
        /// <returns>The <see cref="List{Roles}"/></returns>
        public List<Roles> GetRoleses(int id)
        {
            try
            {
                return (from s
                        in DbContext.RoleActions
                        join s2 in DbContext.Roles on s.RoleId equals s2.RoleId
                        where s.ActionId == id && s.IsTrue
                        select s2).ToList();
            }
            catch (Exception e)
            {
                ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                logger.Debug(e.Message);
                return null;
            }
        }

        /// <summary>
        /// The Search
        /// </summary>
        /// <param name="key">The key<see cref="string"/></param>
        /// <returns>The <see cref="IEnumerable{Action}"/></returns>
        public IEnumerable<Action> Search(string key)
        {
            return base.GetMany(a => a.ActionName.Contains(key) || a.Description.Contains(key));
        }

        /// <summary>
        /// The ScanUpdate
        /// </summary>
        /// <param name="ActionNames">The ActionNames<see cref="List{string}"/></param>
        /// <returns>The <see cref="bool"/></returns>
        public bool ScanUpdate(List<string> ActionNames)
        {
            using (var tran = DbContext.Database.BeginTransaction())
            {
                try
                {
                    DbContext.RoleActions.RemoveRange(
                        DbContext.RoleActions.Where(x => x.Action.Description == "(Tự động quét)")
                    );
                    DbContext.SaveChanges();
                    DbContext.Actions.RemoveRange(
                        DbContext.Actions.Where(x => x.Description == "(Tự động quét)")
                        );
                    DbContext.SaveChanges();
                    String myActionName = "";
                    List<Action> mActions = GetAll().ToList();
                    foreach (var item in mActions)
                        myActionName += item.ActionName;
                    foreach (var item in ActionNames)
                    {
                        if (myActionName.Contains(item))
                            continue;
                        Action myAction = new Action();
                        myAction.ActionId = 0;
                        myAction.ActionName = item;
                        myAction.Description = "(Tự động quét)";
                        Add(myAction);
                        DbContext.SaveChanges();
                    }
                    DbContext.SaveChanges();
                    tran.Commit();
                    return true;
                }
                catch (Exception e)
                {
                    tran.Rollback();
                    ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                    logger.Debug(e.Message);
                    return false;
                }
            }
        }

        /// <summary>
        /// The Update
        /// </summary>
        /// <param name="entity">The entity<see cref="Action"/></param>
        /// <returns>The <see cref="bool"/></returns>
        public bool Update(Action entity)
        {
            try
            {
                Action myAction = GetById(entity.ActionId);
                myAction.Description = entity.Description;
                DbContext.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                logger.Debug(e.Message);
                return false;
            }
        }
    }
}
