namespace TestingSystem.Sevice
{
    using System.Collections.Generic;
    using TestingSystem.Data.Infrastructure;
    using TestingSystem.Data.Repositories;
    using TestingSystem.Models;
    using Action = TestingSystem.Models.Action;

    /// <summary>
    /// Defines the <see cref="IRoleService" />
    /// </summary>
    public interface IRoleService
    {
        /// <summary>
        /// The GetById
        /// </summary>
        /// <param name="id">The id<see cref="int"/></param>
        /// <returns>The <see cref="Roles"/></returns>
        Roles GetById(int id);

        /// <summary>
        /// The CreateRole
        /// </summary>
        /// <param name="role">The role<see cref="TestingSystem.Models.Roles"/></param>
        void CreateRole(TestingSystem.Models.Roles role);

        /// <summary>
        /// The DeleteRole
        /// </summary>
        /// <param name="role">The role<see cref="TestingSystem.Models.Roles"/></param>
        void DeleteRole(TestingSystem.Models.Roles role);

        /// <summary>
        /// The EditRole
        /// </summary>
        /// <param name="role">The role<see cref="TestingSystem.Models.Roles"/></param>
        void EditRole(TestingSystem.Models.Roles role);

        /// <summary>
        /// The GetActionInRole
        /// </summary>
        /// <param name="idRole">The idRole<see cref="int"/></param>
        /// <returns>The <see cref="List{Action}"/></returns>
        List<Action> GetActionInRole(int idRole);

        /// <summary>
        /// The GetAll
        /// </summary>
        /// <returns>The <see cref="List{Roles}"/></returns>
        List<Roles> GetAll();
    }

    /// <summary>
    /// Defines the <see cref="RoleService" />
    /// </summary>
    public class RoleService : IRoleService
    {
        /// <summary>
        /// Defines the roleRepository
        /// </summary>
        private readonly IRoleRepository roleRepository;

        /// <summary>
        /// Defines the unitOfWork
        /// </summary>
        private readonly UnitOfWork unitOfWork;

        /// <summary>
        /// Initializes a new instance of the <see cref="RoleService"/> class.
        /// </summary>
        /// <param name="roleRepository">The roleRepository<see cref="IRoleRepository"/></param>
        /// <param name="unitOfWork">The unitOfWork<see cref="UnitOfWork"/></param>
        public RoleService(IRoleRepository roleRepository, UnitOfWork unitOfWork)
        {
            this.roleRepository = roleRepository;
            this.unitOfWork = unitOfWork;
        }

        /// <summary>
        /// The GetById
        /// </summary>
        /// <param name="id">The id<see cref="int"/></param>
        /// <returns>The <see cref="Roles"/></returns>
        public Roles GetById(int id)
        {
            return roleRepository.GetById(id);
        }

        /// <summary>
        /// The CreateRole
        /// </summary>
        /// <param name="role">The role<see cref="Roles"/></param>
        public void CreateRole(Roles role)
        {
            roleRepository.Add(role);
        }

        /// <summary>
        /// The DeleteRole
        /// </summary>
        /// <param name="role">The role<see cref="Roles"/></param>
        public void DeleteRole(Roles role)
        {
            roleRepository.Delete(role);
        }

        /// <summary>
        /// The EditRole
        /// </summary>
        /// <param name="role">The role<see cref="Roles"/></param>
        public void EditRole(Roles role)
        {
            roleRepository.Edit(role);
        }

        /// <summary>
        /// The GetActionInRole
        /// </summary>
        /// <param name="idRole">The idRole<see cref="int"/></param>
        /// <returns>The <see cref="List{Action}"/></returns>
        public List<Action> GetActionInRole(int idRole)
        {
            return roleRepository.GetActionInRole(idRole);
        }

        /// <summary>
        /// The GetAll
        /// </summary>
        /// <returns>The <see cref="List{Roles}"/></returns>
        public List<Roles> GetAll()
        {
            return roleRepository.GetAll() as List<Roles>;
        }
    }
}
