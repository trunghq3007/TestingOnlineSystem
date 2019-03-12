namespace TestingSystem.Sevice
{
    using System.Collections.Generic;
    using TestingSystem.Data.Infrastructure;
    using TestingSystem.Data.Repositories;
    using TestingSystem.Models;

    /// <summary>
    /// Defines the <see cref="IUserGroupService" />
    /// </summary>
    public interface IUserGroupService
    {
        /// <summary>
        /// The CreateUserGroup
        /// </summary>
        /// <param name="idUser">The idUser<see cref="int"/></param>
        /// <param name="idGroup">The idGroup<see cref="int"/></param>
        /// <param name="isManager">The isManager<see cref="bool"/></param>
        void CreateUserGroup(int idUser, int idGroup, bool isManager);

        /// <summary>
        /// The EditUserGroup
        /// </summary>
        /// <param name="userGroup">The userGroup<see cref="UserGroup"/></param>
        void EditUserGroup(UserGroup userGroup);

        /// <summary>
        /// The DeleteUserGroup
        /// </summary>
        /// <param name="idUser">The idUser<see cref="int"/></param>
        /// <param name="idGroup">The idGroup<see cref="int"/></param>
        /// <param name="isManager">The isManager<see cref="bool"/></param>
        void DeleteUserGroup(int idUser, int idGroup, bool isManager);

        /// <summary>
        /// The GetAllUserGroups
        /// </summary>
        /// <returns>The <see cref="IEnumerable{UserGroup}"/></returns>
        IEnumerable<UserGroup> GetAllUserGroups();

        /// <summary>
        /// The GetAllUserGroupsByIdGroup
        /// </summary>
        /// <param name="id">The id<see cref="int"/></param>
        /// <returns>The <see cref="IEnumerable{UserGroup}"/></returns>
        IEnumerable<UserGroup> GetAllUserGroupsByIdGroup(int id);
    }

    /// <summary>
    /// Defines the <see cref="UserGroupService" />
    /// </summary>
    public class UserGroupService : IUserGroupService
    {
        /// <summary>
        /// Defines the groupUserRepository
        /// </summary>
        private readonly IUserGroupRepository groupUserRepository;

        /// <summary>
        /// Defines the unitOfWork
        /// </summary>
        private readonly IUnitOfWork unitOfWork;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserGroupService"/> class.
        /// </summary>
        /// <param name="groupUserRepository">The groupUserRepository<see cref="IUserGroupRepository"/></param>
        /// <param name="unitOfWork">The unitOfWork<see cref="IUnitOfWork"/></param>
        public UserGroupService(IUserGroupRepository groupUserRepository, IUnitOfWork unitOfWork)
        {
            this.groupUserRepository = groupUserRepository;
            this.unitOfWork = unitOfWork;
        }

        /// <summary>
        /// The CreateUserGroup
        /// </summary>
        /// <param name="idUser">The idUser<see cref="int"/></param>
        /// <param name="idGroup">The idGroup<see cref="int"/></param>
        /// <param name="isManager">The isManager<see cref="bool"/></param>
        public void CreateUserGroup(int idUser, int idGroup, bool isManager)
        {
            var list = groupUserRepository.GetAll();
            groupUserRepository.CreateUserGroup(idUser, idGroup, isManager);
        }

        /// <summary>
        /// The DeleteUserGroup
        /// </summary>
        /// <param name="idUser">The idUser<see cref="int"/></param>
        /// <param name="idGroup">The idGroup<see cref="int"/></param>
        /// <param name="isManager">The isManager<see cref="bool"/></param>
        public void DeleteUserGroup(int idUser, int idGroup, bool isManager)
        {
            groupUserRepository.DeleteUserGroup(idUser, idGroup, isManager);
        }

        /// <summary>
        /// The EditUserGroup
        /// </summary>
        /// <param name="userGroup">The userGroup<see cref="UserGroup"/></param>
        public void EditUserGroup(UserGroup userGroup)
        {
            groupUserRepository.EditUserGroup(userGroup);
        }

        /// <summary>
        /// The GetAllUserGroups
        /// </summary>
        /// <returns>The <see cref="IEnumerable{UserGroup}"/></returns>
        public IEnumerable<UserGroup> GetAllUserGroups()
        {
            return groupUserRepository.GetAll();
        }

        /// <summary>
        /// The GetAllUserGroupsByIdGroup
        /// </summary>
        /// <param name="id">The id<see cref="int"/></param>
        /// <returns>The <see cref="IEnumerable{UserGroup}"/></returns>
        public IEnumerable<UserGroup> GetAllUserGroupsByIdGroup(int id)
        {
            return groupUserRepository.GetAllUserGroupsByIdGroup(id);
        }
    }
}
