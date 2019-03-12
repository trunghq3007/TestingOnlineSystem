namespace TestingSystem.Data.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using TestingSystem.Data.Infrastructure;
    using TestingSystem.Models;

    /// <summary>
    /// Defines the <see cref="IUserGroupRepository" />
    /// </summary>
    public interface IUserGroupRepository : IRepository<UserGroup>
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
        /// The GetAllUserGroupsByIdGroup
        /// </summary>
        /// <param name="id">The id<see cref="int"/></param>
        /// <returns>The <see cref="IEnumerable{UserGroup}"/></returns>
        IEnumerable<UserGroup> GetAllUserGroupsByIdGroup(int id);
    }

    /// <summary>
    /// Defines the <see cref="UserGroupRepository" />
    /// </summary>
    public class UserGroupRepository : RepositoryBase<UserGroup>, IUserGroupRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserGroupRepository"/> class.
        /// </summary>
        /// <param name="dbFactory">The dbFactory<see cref="IDbFactory"/></param>
        public UserGroupRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

        /// <summary>
        /// The CreateUserGroup
        /// </summary>
        /// <param name="idUser">The idUser<see cref="int"/></param>
        /// <param name="idGroup">The idGroup<see cref="int"/></param>
        /// <param name="isManager">The isManager<see cref="bool"/></param>
        public void CreateUserGroup(int idUser, int idGroup, bool isManager)
        {
            var checkAdd = true;
            var list = GetAll();
            foreach (var item in list)
            {
                if (idUser == item.UserId && idGroup == item.GroupId)
                {
                    checkAdd = false;
                }
            }
            if (checkAdd == true)
            {
                UserGroup item = new UserGroup();
                item.UserId = idUser;
                item.GroupId = idGroup;
                item.IsManager = isManager;
                this.DbContext.UserGroups.Add(item);
                this.DbContext.SaveChanges();
            }
        }

        /// <summary>
        /// The EditUserGroup
        /// </summary>
        /// <param name="userGroup">The userGroup<see cref="UserGroup"/></param>
        public void EditUserGroup(UserGroup userGroup)
        {
            base.Update(userGroup);
        }

        /// <summary>
        /// The DeleteUserGroup
        /// </summary>
        /// <param name="idUser">The idUser<see cref="int"/></param>
        /// <param name="idGroup">The idGroup<see cref="int"/></param>
        /// <param name="isManager">The isManager<see cref="bool"/></param>
        public void DeleteUserGroup(int idUser, int idGroup, bool isManager)
        {
            var list = this.DbContext.UserGroups.ToList();
            foreach (var item in list)
            {
                if (item.UserId == idUser)
                {
                    this.DbContext.UserGroups.Remove(item);
                    this.DbContext.SaveChanges();
                    break;
                }
            }
        }

        /// <summary>
        /// The Add
        /// </summary>
        /// <param name="entity">The entity<see cref="UserGroup"/></param>
        public void Add(UserGroup entity)
        {
            base.Add(entity);
        }

        /// <summary>
        /// The Update
        /// </summary>
        /// <param name="entity">The entity<see cref="UserGroup"/></param>
        public void Update(UserGroup entity)
        {
            base.Update(entity);
        }

        /// <summary>
        /// The Delete
        /// </summary>
        /// <param name="entity">The entity<see cref="UserGroup"/></param>
        public void Delete(UserGroup entity)
        {
            base.Delete(entity);
        }

        /// <summary>
        /// The Delete
        /// </summary>
        /// <param name="@where">The where<see cref="Expression{Func{UserGroup, bool}}"/></param>
        public void Delete(Expression<Func<UserGroup, bool>> @where)
        {
            base.Delete(@where);
        }

        /// <summary>
        /// The GetById
        /// </summary>
        /// <param name="id">The id<see cref="int"/></param>
        /// <returns>The <see cref="UserGroup"/></returns>
        public UserGroup GetById(int id)
        {
            return base.GetById(id);
        }

        /// <summary>
        /// The Get
        /// </summary>
        /// <param name="@where">The where<see cref="Expression{Func{UserGroup, bool}}"/></param>
        /// <returns>The <see cref="UserGroup"/></returns>
        public UserGroup Get(Expression<Func<UserGroup, bool>> @where)
        {
            return base.Get(@where);
        }

        /// <summary>
        /// The GetAll
        /// </summary>
        /// <returns>The <see cref="IEnumerable{UserGroup}"/></returns>
        public IEnumerable<UserGroup> GetAll()
        {
            return base.GetAll();
        }

        /// <summary>
        /// The GetMany
        /// </summary>
        /// <param name="@where">The where<see cref="Expression{Func{UserGroup, bool}}"/></param>
        /// <returns>The <see cref="IEnumerable{UserGroup}"/></returns>
        public IEnumerable<UserGroup> GetMany(Expression<Func<UserGroup, bool>> @where)
        {
            return base.GetMany(@where);
        }

        /// <summary>
        /// The GetAllUserGroupsByIdGroup
        /// </summary>
        /// <param name="id">The id<see cref="int"/></param>
        /// <returns>The <see cref="IEnumerable{UserGroup}"/></returns>
        public IEnumerable<UserGroup> GetAllUserGroupsByIdGroup(int id)
        {
            var list = this.DbContext.UserGroups.Where(s => s.GroupId == id).ToList();
            return list;
        }
    }
}
