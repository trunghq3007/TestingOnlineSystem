namespace TestingSystem.Sevice
{
    using System.Collections.Generic;
    using System.Linq;
    using TestingSystem.Data.Infrastructure;
    using TestingSystem.Data.Repositories;
    using TestingSystem.DataTranferObject;
    using TestingSystem.Models;

    /// <summary>
    /// Defines the <see cref="IGroupService" />
    /// </summary>
    public interface IGroupService
    {
        // function create group
        /// <summary>
        /// The CreateGroup
        /// </summary>
        /// <param name="group">The group<see cref="Group"/></param>
        int CreateGroup(Group group);

        // function edit information group
        /// <summary>
        /// The EditGroup
        /// </summary>
        /// <param name="group">The group<see cref="Group"/></param>
        int EditGroup(Group group);

        // function delete group by id
        /// <summary>
        /// The DeleteGroup
        /// </summary>
        /// <param name="id">The id<see cref="int"/></param>
        /// <returns>The <see cref="bool"/></returns>
        int DeleteGroup(int id);

        // function get group by id
        /// <summary>
        /// The GetGroupById
        /// </summary>
        /// <param name="id">The id<see cref="int"/></param>
        /// <returns>The <see cref="Group"/></returns>
        Group GetGroupById(int id);

        // function get group with filter
        /// <summary>
        /// The GetAllGroup
        /// </summary>
        /// <param name="groupFilter">The groupFilter<see cref="GroupFilter"/></param>
        /// <returns>The <see cref="List{Group}"/></returns>
        List<Group> GetAllGroup(GroupFilter groupFilter);

        // function get all user in group by id group
        /// <summary>
        /// The GetAllUserInGroup
        /// </summary>
        /// <param name="id">The id<see cref="int"/></param>
        /// <returns>The <see cref="IEnumerable{User}"/></returns>
        IEnumerable<User> GetAllUserInGroup(int id);

        // function get all user in group by name group
        /// <summary>
        /// The GetAllGroupByName
        /// </summary>
        /// <param name="nameGroup">The nameGroup<see cref="string"/></param>
        /// <returns>The <see cref="IEnumerable{Group}"/></returns>
        IEnumerable<Group> GetAllGroupByName(string nameGroup);

        // function get all group
        /// <summary>
        /// The GetAllGroupC
        /// </summary>
        /// <returns>The <see cref="IEnumerable{Group}"/></returns>
        IEnumerable<Group> GetAllGroupC();

        /// <summary>
        /// The GetAllGroupByNameOutId
        /// </summary>
        /// <param name="nameGroup">The nameGroup<see cref="string"/></param>
        /// <param name="idGroup">The idGroup<see cref="int"/></param>
        /// <returns>The <see cref="IEnumerable{Group}"/></returns>
        IEnumerable<Group> GetAllGroupByNameOutId(string nameGroup, int idGroup);
    }

    /// <summary>
    /// Defines the <see cref="GroupService" />
    /// </summary>
    public class GroupService : IGroupService
    {
        /// <summary>
        /// Defines the groupRepository
        /// </summary>
        private readonly IGroupRepository groupRepository;

        /// <summary>
        /// Defines the unitOfWork
        /// </summary>
        private readonly IUnitOfWork unitOfWork;

        /// <summary>
        /// Initializes a new instance of the <see cref="GroupService"/> class.
        /// </summary>
        /// <param name="groupRepository">The groupRepository<see cref="IGroupRepository"/></param>
        /// <param name="unitOfWork">The unitOfWork<see cref="IUnitOfWork"/></param>
        public GroupService(IGroupRepository groupRepository, IUnitOfWork unitOfWork)
        {
            this.groupRepository = groupRepository;
            this.unitOfWork = unitOfWork;
        }

        /// <summary>
        /// The CreateGroup
        /// </summary>
        /// <param name="group">The group<see cref="Group"/></param>
        public int CreateGroup(Group group)
        {
            return groupRepository.CreateGroup(group);
        }

        /// <summary>
        /// The DeleteGroup
        /// </summary>
        /// <param name="id">The id<see cref="int"/></param>
        /// <returns>The <see cref="bool"/></returns>
        public int DeleteGroup(int id)
        {
            var listUser = groupRepository.GetAllUserInGroup(id);
            if (listUser.Count() == 0)
            {
                groupRepository.DeleteGroup(id);
                return 1;
            }
            return 0;
        }

        /// <summary>
        /// The EditGroup
        /// </summary>
        /// <param name="group">The group<see cref="Group"/></param>
        public int EditGroup(Group group)
        {
            return groupRepository.EditGroup(group);
        }

        /// <summary>
        /// The GetAllGroup
        /// </summary>
        /// <param name="groupFilter">The groupFilter<see cref="GroupFilter"/></param>
        /// <returns>The <see cref="List{Group}"/></returns>
        public List<Group> GetAllGroup(GroupFilter groupFilter)
        {
            return groupRepository.GetAllGroup(groupFilter);
        }

        /// <summary>
        /// The GetAllUserInGroup
        /// </summary>
        /// <param name="id">The id<see cref="int"/></param>
        /// <returns>The <see cref="IEnumerable{User}"/></returns>
        public IEnumerable<User> GetAllUserInGroup(int id)
        {
            return groupRepository.GetAllUserInGroup(id);
        }

        /// <summary>
        /// The GetGroupById
        /// </summary>
        /// <param name="id">The id<see cref="int"/></param>
        /// <returns>The <see cref="Group"/></returns>
        public Group GetGroupById(int id)
        {
            return groupRepository.GetById(id);
        }

        /// <summary>
        /// The GetAllGroupByName
        /// </summary>
        /// <param name="nameGroup">The nameGroup<see cref="string"/></param>
        /// <returns>The <see cref="IEnumerable{Group}"/></returns>
        public IEnumerable<Group> GetAllGroupByName(string nameGroup)
        {
            return groupRepository.GetAllGroupByName(nameGroup);
        }

        /// <summary>
        /// The GetAllGroupC
        /// </summary>
        /// <returns>The <see cref="IEnumerable{Group}"/></returns>
        public IEnumerable<Group> GetAllGroupC()
        {
            return groupRepository.GetAllGroupC();
        }

        /// <summary>
        /// The GetAllGroupByNameOutId
        /// </summary>
        /// <param name="nameGroup">The nameGroup<see cref="string"/></param>
        /// <param name="idGroup">The idGroup<see cref="int"/></param>
        /// <returns>The <see cref="IEnumerable{Group}"/></returns>
        public IEnumerable<Group> GetAllGroupByNameOutId(string nameGroup, int idGroup)
        {
            return groupRepository.GetAllGroupByNameOutId(nameGroup, idGroup);
        }
    }
}
