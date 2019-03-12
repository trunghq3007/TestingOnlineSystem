namespace TestingSystem.Sevice
{
    using System.Collections.Generic;
    using TestingSystem.Data.Infrastructure;
    using TestingSystem.Data.Repositories;
    using TestingSystem.Models;

    /// <summary>
    /// Defines the <see cref="IRoleActionService" />
    /// </summary>
    public interface IRoleActionService
    {
        /// <summary>
        /// The CreateRoleAction
        /// </summary>
        /// <param name="roleAction">The roleAction<see cref="RoleAction"/></param>
        void CreateRoleAction(RoleAction roleAction);

        /// <summary>
        /// The EditRoleAction
        /// </summary>
        /// <param name="roleAction">The roleAction<see cref="RoleAction"/></param>
        void EditRoleAction(RoleAction roleAction);

        /// <summary>
        /// The DeleteRoleAction
        /// </summary>
        /// <param name="roleAction">The roleAction<see cref="RoleAction"/></param>
        void DeleteRoleAction(RoleAction roleAction);

        /// <summary>
        /// The GetAll
        /// </summary>
        /// <returns>The <see cref="IEnumerable{RoleAction}"/></returns>
        IEnumerable<RoleAction> GetAll();

        /// <summary>
        /// The Update
        /// </summary>
        /// <param name="list">The list<see cref="string[]"/></param>
        /// <returns>The <see cref="bool"/></returns>
        bool Update(string[] list);
    }

    /// <summary>
    /// Defines the <see cref="RoleActionService" />
    /// </summary>
    public class RoleActionService : IRoleActionService
    {
        /// <summary>
        /// Defines the roleActionRepository
        /// </summary>
        private readonly IRoleActionRepository roleActionRepository;

        /// <summary>
        /// Defines the unitOfWork
        /// </summary>
        private readonly IUnitOfWork unitOfWork;

        /// <summary>
        /// Initializes a new instance of the <see cref="RoleActionService"/> class.
        /// </summary>
        /// <param name="roleActionRepository">The roleActionRepository<see cref="IRoleActionRepository"/></param>
        /// <param name="unitOfWork">The unitOfWork<see cref="IUnitOfWork"/></param>
        public RoleActionService(IRoleActionRepository roleActionRepository, IUnitOfWork unitOfWork)
        {
            this.roleActionRepository = roleActionRepository;
            this.unitOfWork = unitOfWork;
        }

        /// <summary>
        /// The CreateRoleAction
        /// </summary>
        /// <param name="roleAction">The roleAction<see cref="RoleAction"/></param>
        public void CreateRoleAction(RoleAction roleAction)
        {
            roleActionRepository.Add(roleAction);
        }

        /// <summary>
        /// The EditRoleAction
        /// </summary>
        /// <param name="roleAction">The roleAction<see cref="RoleAction"/></param>
        public void EditRoleAction(RoleAction roleAction)
        {
            roleActionRepository.Update(roleAction);
        }

        /// <summary>
        /// The DeleteRoleAction
        /// </summary>
        /// <param name="roleAction">The roleAction<see cref="RoleAction"/></param>
        public void DeleteRoleAction(RoleAction roleAction)
        {
            roleActionRepository.Delete(roleAction);
        }

        /// <summary>
        /// The GetAll
        /// </summary>
        /// <returns>The <see cref="IEnumerable{RoleAction}"/></returns>
        public IEnumerable<RoleAction> GetAll()
        {
            return roleActionRepository.GetAll();
        }

        /// <summary>
        /// The Update
        /// </summary>
        /// <param name="list">The list<see cref="string[]"/></param>
        /// <returns>The <see cref="bool"/></returns>
        public bool Update(string[] list)
        {
            return roleActionRepository.Update(list);
        }
    }
}
