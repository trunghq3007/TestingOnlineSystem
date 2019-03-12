namespace TestingSystem.Sevice
{
    using System.Collections.Generic;
    using TestingSystem.Data.Infrastructure;
    using TestingSystem.Data.Repositories;
    using TestingSystem.Models;
    using Action = TestingSystem.Models.Action;

    /// <summary>
    /// Defines the <see cref="IActionService" />
    /// </summary>
    public interface IActionService
    {
        /// <summary>
        /// The CreateAction
        /// </summary>
        /// <param name="action">The action<see cref="Action"/></param>
        void CreateAction(Action action);

        /// <summary>
        /// The EditAction
        /// </summary>
        /// <param name="action">The action<see cref="Action"/></param>
        bool EditAction(Action action);

        /// <summary>
        /// The GetAllActions
        /// </summary>
        /// <returns>The <see cref="IEnumerable{Action}"/></returns>
        IEnumerable<Action> GetAllActions();

        /// <summary>
        /// The AddActionToRole
        /// </summary>
        /// <param name="idAction">The idAction<see cref="int"/></param>
        /// <param name="idRole">The idRole<see cref="int"/></param>
        void AddActionToRole(int idAction, int idRole);

        /// <summary>
        /// The DeleteAction
        /// </summary>
        /// <param name="id">The id<see cref="int"/></param>
        void DeleteAction(int id);

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
    }

    /// <summary>
    /// Defines the <see cref="ActionService" />
    /// </summary>
    public class ActionService : IActionService
    {
        /// <summary>
        /// Defines the actionRepository
        /// </summary>
        private readonly IActionRepository actionRepository;

        /// <summary>
        /// Defines the unitOfWork
        /// </summary>
        private readonly IUnitOfWork unitOfWork;

        /// <summary>
        /// Initializes a new instance of the <see cref="ActionService"/> class.
        /// </summary>
        /// <param name="actionRepository">The actionRepository<see cref="IActionRepository"/></param>
        /// <param name="unitOfWork">The unitOfWork<see cref="IUnitOfWork"/></param>
        public ActionService(IActionRepository actionRepository, IUnitOfWork unitOfWork)
        {
            this.actionRepository = actionRepository;
            this.unitOfWork = unitOfWork;
        }

        /// <summary>
        /// The CreateAction
        /// </summary>
        /// <param name="action">The action<see cref="Action"/></param>
        public void CreateAction(Action action)
        {
            actionRepository.Add(action);
        }

        /// <summary>
        /// The EditAction
        /// </summary>
        /// <param name="action">The action<see cref="Action"/></param>
        public bool EditAction(Action action)
        {
            return actionRepository.Update(action);
        }

        /// <summary>
        /// The DeleteAction
        /// </summary>
        /// <param name="id">The id<see cref="int"/></param>
        public void DeleteAction(int id)
        {
            actionRepository.Delete(id);
        }

        /// <summary>
        /// The GetAllActions
        /// </summary>
        /// <returns>The <see cref="IEnumerable{Action}"/></returns>
        public IEnumerable<Action> GetAllActions()
        {
            var list = actionRepository.GetAll();
            return list;
        }

        /// <summary>
        /// The AddActionToRole
        /// </summary>
        /// <param name="idAction">The idAction<see cref="int"/></param>
        /// <param name="idRole">The idRole<see cref="int"/></param>
        public void AddActionToRole(int idAction, int idRole)
        {
            actionRepository.AddActionToRole(idAction, idRole);
        }

        /// <summary>
        /// The Get
        /// </summary>
        /// <param name="id">The id<see cref="int"/></param>
        /// <returns>The <see cref="Action"/></returns>
        public Action Get(int id)
        {
            return actionRepository.Get(id);
        }

        /// <summary>
        /// The GetRoleses
        /// </summary>
        /// <param name="id">The id<see cref="int"/></param>
        /// <returns>The <see cref="List{Roles}"/></returns>
        public List<Roles> GetRoleses(int id)
        {
            return actionRepository.GetRoleses(id);
        }

        /// <summary>
        /// The Search
        /// </summary>
        /// <param name="key">The key<see cref="string"/></param>
        /// <returns>The <see cref="IEnumerable{Action}"/></returns>
        public IEnumerable<Action> Search(string key)
        {
            return actionRepository.Search(key);
        }

        /// <summary>
        /// The ScanUpdate
        /// </summary>
        /// <param name="ActionNames">The ActionNames<see cref="List{string}"/></param>
        /// <returns>The <see cref="bool"/></returns>
        public bool ScanUpdate(List<string> ActionNames)
        {
            return actionRepository.ScanUpdate(ActionNames);
        }
    }
}
