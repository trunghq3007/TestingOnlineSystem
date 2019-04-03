namespace TestingSystem.Sevice
{
    using System.Collections.Generic;
    using TestingSystem.Data.Infrastructure;
    using TestingSystem.Data.Repositories;
    using TestingSystem.DataTranferObject;
    using TestingSystem.Models;

    /// <summary>
    /// Defines the <see cref="IUserService" />
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// The Active
        /// </summary>
        /// <param name="key">The key<see cref="string"/></param>
        /// <returns>The <see cref="int"/></returns>
        int Active(string key);

        /// <summary>
        /// The GetUser
        /// </summary>
        /// <param name="id">The id<see cref="int"/></param>
        /// <returns>The <see cref="User"/></returns>
        User GetUser(int id);

        /// <summary>
        /// The Login
        /// </summary>
        /// <param name="user">The user<see cref="UserLogin"/></param>
        /// <returns>The <see cref="int"/></returns>
        int Login(UserLogin user);

        /// <summary>
        /// The GetAction
        /// </summary>
        /// <param name="userId">The userId<see cref="int"/></param>
        /// <returns>The <see cref="List{RoleAction}"/></returns>
        List<RoleAction> GetAction(int userId);

        /// <summary>
        /// The GetRoleId
        /// </summary>
        /// <param name="userId">The userId<see cref="int"/></param>
        /// <returns>The <see cref="int"/></returns>
        int GetRoleId(int userId);

        /// <summary>
        /// The Register
        /// </summary>
        /// <param name="user">The user<see cref="UserRegister"/></param>
        /// <returns>The <see cref="bool"/></returns>
        bool Register(UserRegister user);

        /// <summary>
        /// The AddUser
        /// </summary>
        /// <param name="entity">The entity<see cref="User"/></param>
        /// <returns>The <see cref="int"/></returns>
        int AddUser(User entity);

        /// <summary>
        /// The ListAll
        /// </summary>
        /// <returns>The <see cref="List{User}"/></returns>
        List<User> ListAll();

        /// <summary>
        /// The GetUserById
        /// </summary>
        /// <param name="UserId">The UserId<see cref="int"/></param>
        /// <returns>The <see cref="User"/></returns>
        User GetUserById(int UserId);

        /// <summary>
        /// The ListGroupsOfUser
        /// </summary>
        /// <param name="user">The user<see cref="User"/></param>
        /// <returns>The <see cref="List{Group}"/></returns>
        List<Group> ListGroupsOfUser(User user);

        /// <summary>
        /// The ListAllActive
        /// </summary>
        /// <returns>The <see cref="List{User}"/></returns>
        List<User> ListAllActive();

        /// <summary>
        /// The DeleteUser
        /// </summary>
        /// <param name="UserId">The UserId<see cref="int"/></param>
        /// <returns>The <see cref="int"/></returns>
        int DeleteUser(int UserId);

        /// <summary>
        /// The EditUser
        /// </summary>
        /// <param name="user">The user<see cref="User"/></param>
        /// <returns>The <see cref="int"/></returns>
        int EditUser(User user);

        /// <summary>
        /// The ListAll
        /// </summary>
        /// <param name="SearchString">The SearchString<see cref="string"/></param>
        /// <returns>The <see cref="List{User}"/></returns>
        List<User> ListAll(string SearchString);

        /// <summary>
        /// The Filter
        /// </summary>
        /// <param name="filterstring">The filterstring<see cref="string"/></param>
        /// <returns>The <see cref="List{User}"/></returns>
        List<User> Filter(string filterstring);

        /// <summary>
        /// The ListAllDisable
        /// </summary>
        /// <returns>The <see cref="List{User}"/></returns>
        List<User> ListAllDisable();

        /// <summary>
        /// The CountUser
        /// </summary>
        /// <returns>The <see cref="int"/></returns>
        int CountUser();

        bool Recovery(string email);
        bool Reset(string email, string pass);
        List<User> GetAllUserRoleIsMemberOrSubMember();
        List<User> GetAllUserRoleIsMemberOrSubMemberByKeySearch(string keySearch);

    }

    /// <summary>
    /// Defines the <see cref="UserService" />
    /// </summary>
    public class UserService : IUserService
    {
        /// <summary>
        /// Defines the userRepository
        /// </summary>
        private readonly IUserRepository userRepository;

        /// <summary>
        /// Defines the unitOfWork
        /// </summary>
        private readonly IUnitOfWork unitOfWork;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserService"/> class.
        /// </summary>
        /// <param name="userRepository">The userRepository<see cref="IUserRepository"/></param>
        /// <param name="unitOfWork">The unitOfWork<see cref="IUnitOfWork"/></param>
        public UserService(IUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            this.userRepository = userRepository;
            this.unitOfWork = unitOfWork;
        }

        /// <summary>
        /// The Login
        /// </summary>
        /// <param name="user">The user<see cref="UserLogin"/></param>
        /// <returns>The <see cref="int"/></returns>
        public int Login(UserLogin user)
        {
            return userRepository.Login(user);
        }

        /// <summary>
        /// The GetAction
        /// </summary>
        /// <param name="userId">The userId<see cref="int"/></param>
        /// <returns>The <see cref="List{RoleAction}"/></returns>
        public List<RoleAction> GetAction(int userId)
        {
            return userRepository.GetAction(userId);
        }

        /// <summary>
        /// The GetRoleId
        /// </summary>
        /// <param name="userId">The userId<see cref="int"/></param>
        /// <returns>The <see cref="int"/></returns>
        public int GetRoleId(int userId)
        {
            return userRepository.GetRoleId(userId);
        }

        /// <summary>
        /// The GetUser
        /// </summary>
        /// <param name="id">The id<see cref="int"/></param>
        /// <returns>The <see cref="User"/></returns>
        public User GetUser(int id)
        {
            return userRepository.GetById(id);
        }

        /// <summary>
        /// The Register
        /// </summary>
        /// <param name="user">The user<see cref="UserRegister"/></param>
        /// <returns>The <see cref="bool"/></returns>
        public bool Register(UserRegister user)
        {
            return userRepository.Register(user);
        }

        /// <summary>
        /// The Active
        /// </summary>
        /// <param name="key">The key<see cref="string"/></param>
        /// <returns>The <see cref="int"/></returns>
        public int Active(string key)
        {
            return userRepository.Active(key);
        }

        /// <summary>
        /// The AddUser
        /// </summary>
        /// <param name="entity">The entity<see cref="User"/></param>
        /// <returns>The <see cref="int"/></returns>
        public int AddUser(User entity)
        {
            return userRepository.AddUser(entity);
        }

        /// <summary>
        /// The ListAllActive
        /// </summary>
        /// <returns>The <see cref="List{User}"/></returns>
        public List<User> ListAllActive()
        {
            return userRepository.ListAllActive();
        }

        /// <summary>
        /// The DeleteUser
        /// </summary>
        /// <param name="userId">The userId<see cref="int"/></param>
        /// <returns>The <see cref="int"/></returns>
        public int DeleteUser(int userId)
        {
            return userRepository.DeleteUser(userId);
        }

        /// <summary>
        /// The GetUserById
        /// </summary>
        /// <param name="UserId">The UserId<see cref="int"/></param>
        /// <returns>The <see cref="User"/></returns>
        public User GetUserById(int UserId)
        {
            return userRepository.GetUserById(UserId);
        }

        /// <summary>
        /// The EditUser
        /// </summary>
        /// <param name="user">The user<see cref="User"/></param>
        /// <returns>The <see cref="int"/></returns>
        public int EditUser(User user)
        {
            return userRepository.EditUser(user);
        }

        /// <summary>
        /// The ListAll
        /// </summary>
        /// <returns>The <see cref="List{User}"/></returns>
        public List<User> ListAll()
        {
            return userRepository.ListAll();
        }

        /// <summary>
        /// The ListAll
        /// </summary>
        /// <param name="SearchString">The SearchString<see cref="string"/></param>
        /// <returns>The <see cref="List{User}"/></returns>
        public List<User> ListAll(string SearchString)
        {
            return userRepository.SearchUser(SearchString);
        }

        /// <summary>
        /// The ListGroupsOfUser
        /// </summary>
        /// <param name="user">The user<see cref="User"/></param>
        /// <returns>The <see cref="List{Group}"/></returns>
        public List<Group> ListGroupsOfUser(User user)
        {
            return userRepository.ListGroups(user);
        }

        /// <summary>
        /// The Filter
        /// </summary>
        /// <param name="filterstring">The filterstring<see cref="string"/></param>
        /// <returns>The <see cref="List{User}"/></returns>
        public List<User> Filter(string filterstring)
        {
            return userRepository.Filter(filterstring);
        }

        /// <summary>
        /// The CountUser
        /// </summary>
        /// <returns>The <see cref="int"/></returns>
        public int CountUser()
        {
            return userRepository.CountUser();
        }

        /// <summary>
        /// The ListAllDisable
        /// </summary>
        /// <returns>The <see cref="List{User}"/></returns>
        public List<User> ListAllDisable()
        {
            return userRepository.ListAllDisable();
        }

        public bool Recovery(string email)
        {
            return userRepository.Recovery(email);
        }

        public bool Reset(string email, string pass)
        {
            return userRepository.Reset(email, pass);
        }

        public List<User> GetAllUserRoleIsMemberOrSubMember()
        {
	        return userRepository.GetAllUserRoleIsMemberOrSubMember();
        }

        public List<User> GetAllUserRoleIsMemberOrSubMemberByKeySearch(string keySearch)
        {
	        return userRepository.GetAllUserRoleIsMemberOrSubMemberByKeySearch(keySearch);
        }
    }
}
