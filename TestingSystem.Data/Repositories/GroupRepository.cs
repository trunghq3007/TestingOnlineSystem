namespace TestingSystem.Data.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using TestingSystem.Data.Infrastructure;
    using TestingSystem.DataTranferObject;
    using TestingSystem.Models;

    /// <summary>
    /// Defines the <see cref="IGroupRepository" />
    /// </summary>
    public interface IGroupRepository : IRepository<Group>
    {
        /// <summary>
        /// The GetAllUserInGroup
        /// </summary>
        /// <param name="idGroup">The idGroup<see cref="int"/></param>
        /// <returns>The <see cref="IEnumerable{User}"/></returns>
        IEnumerable<User> GetAllUserInGroup(int idGroup);

        /// <summary>
        /// The Del
        /// </summary>
        /// <param name="id">The id<see cref="int"/></param>
        int DeleteGroup(int id);

        int CreateGroup(Group group);

        int EditGroup(Group group);

        /// <summary>
        /// The GetAllGroupByName
        /// </summary>
        /// <param name="nameGroup">The nameGroup<see cref="string"/></param>
        /// <returns>The <see cref="IEnumerable{Group}"/></returns>
        IEnumerable<Group> GetAllGroupByName(string nameGroup);

        /// <summary>
        /// The GetAllGroup
        /// </summary>
        /// <param name="groupfilter">The groupfilter<see cref="GroupFilter"/></param>
        /// <returns>The <see cref="List{Group}"/></returns>
        List<Group> GetAllGroup(GroupFilter groupfilter);

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
    /// Defines the <see cref="GroupRepository" />
    /// </summary>
    public class GroupRepository : RepositoryBase<Group>, IGroupRepository
    {
        /// <summary>
        /// Defines the log
        /// </summary>
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Initializes a new instance of the <see cref="GroupRepository"/> class.
        /// </summary>
        /// <param name="dbFactory">The dbFactory<see cref="IDbFactory"/></param>
        public GroupRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

        /// <summary>
        /// The GetAllUserInGroup
        /// </summary>
        /// <param name="idGroup">The idGroup<see cref="int"/></param>
        /// <returns>The <see cref="IEnumerable{User}"/></returns>
        public IEnumerable<User> GetAllUserInGroup(int idGroup)
        {
            try
            {
                // query all usergroup by idGroup
                var list = this.DbContext.UserGroups.Where(c => c.GroupId == idGroup).ToList();
                var listUser = this.DbContext.Users.ToList();
                List<User> listUserInGroup = new List<User>();
                foreach (var item in list)
                {
                    foreach (var item2 in listUser)
                    {
                        // compare userid in usergroup with userid in user
                        if (item2.UserId == item.UserId)
                        {
                            listUserInGroup.Add(item2);
                        }
                    }
                }
                return listUserInGroup;
            }
            catch (Exception e)
            {
                log.Debug(e.Message);
                return null;
            }
        }

        /// <summary>
        /// The GetAllGroup
        /// </summary>
        /// <param name="groupfilter">The groupfilter<see cref="GroupFilter"/></param>
        /// <returns>The <see cref="List{Group}"/></returns>
        public List<Group> GetAllGroup(GroupFilter groupfilter)
        {
            try
            {
                // get all group
                List<Group> data = null;
                if (groupfilter.StartCreatedDate == null && groupfilter.EndCreatedDate == null)
                {
                    data = (from S in this.DbContext.Groups
                            select S).ToList();
                }
                else
                if (groupfilter.StartCreatedDate == DateTime.MinValue)
                {
                    data = (from S in this.DbContext.Groups
                            where S.CreatedDate < groupfilter.EndCreatedDate
                            select S).ToList();
                }
                else
                if (groupfilter.EndCreatedDate == DateTime.MinValue)
                {
                    data = (from S in this.DbContext.Groups
                            where S.CreatedDate > groupfilter.StartCreatedDate
                            select S).ToList();
                }
                else
                {
                    data = (from S in this.DbContext.Groups
                            where (S.CreatedDate >= groupfilter.StartCreatedDate
                                      && S.CreatedDate <= groupfilter.EndCreatedDate)
                            select S).ToList();

                }
                return data;

            }
            catch (Exception e)
            {
                log.Debug(e.Message);
                return null;
            }
        }

        /// <summary>
        /// The Add
        /// </summary>
        /// <param name="group">The group<see cref="Group"/></param>
        public int CreateGroup(Group group)
        {
            try
            {
                // add group
                DbContext.Groups.Add(new Group()
                {
                    GroupName = group.GroupName,
                    CreatedDate = group.CreatedDate,
                    UpdatedDate = group.UpdatedDate,
                    Description = group.Description,
                });
                DbContext.SaveChanges();
                return 1;

            }
            catch (Exception e)
            {
                log.Debug(e.Message);
                return 0;
            }
        }

        /// <summary>
        /// The Update
        /// </summary>
        /// <param name="group">The group<see cref="Group"/></param>
        public int EditGroup(Group group)
        {
            try
            {
                var listGroup = this.DbContext.Groups.ToList();
                foreach (var item in listGroup)
                {
                    // find group by id
                    if (item.GroupId == group.GroupId)
                    {
                        item.GroupName = group.GroupName;
                        item.CreatedDate = group.CreatedDate;
                        item.UpdatedDate = group.UpdatedDate;
                        item.Description = group.Description;
                    }
                }
                DbContext.SaveChanges();
                return 1;
            }
            catch (Exception e)
            {
                log.Debug(e.Message);
                return 0;
            }
        }

        /// <summary>
        /// The Del
        /// </summary>
        /// <param name="id">The id<see cref="int"/></param>
        public int DeleteGroup(int id)
        {
            try
            {
                var listGroup = this.DbContext.Groups.ToList();
                foreach (var item in listGroup)
                {
                    if (item.GroupId == id)
                    {
                        DbContext.Groups.Remove(item);
                        DbContext.SaveChanges();
                        break;
                    }
                }
                return 1;
            }
            catch (Exception e)
            {
                log.Debug(e.Message);
                return 0;
            }
        }

        /// <summary>
        /// The GetAllGroupByName
        /// </summary>
        /// <param name="nameGroup">The nameGroup<see cref="string"/></param>
        /// <returns>The <see cref="IEnumerable{Group}"/></returns>
        public IEnumerable<Group> GetAllGroupByName(string nameGroup)
        {
            try
            {
                return this.DbContext.Groups.Where(s => s.GroupName.Contains(nameGroup)).ToList();
            }
            catch (Exception e)
            {
                log.Debug(e.Message);
                return null;
            }
        }

        /// <summary>
        /// The GetAllGroupC
        /// </summary>
        /// <returns>The <see cref="IEnumerable{Group}"/></returns>
        public IEnumerable<Group> GetAllGroupC()
        {
            return this.DbContext.Groups.ToList();
        }

        /// <summary>
        /// The GetAllGroupByNameOutId
        /// </summary>
        /// <param name="nameGroup">The nameGroup<see cref="string"/></param>
        /// <param name="idGroup">The idGroup<see cref="int"/></param>
        /// <returns>The <see cref="IEnumerable{Group}"/></returns>
        public IEnumerable<Group> GetAllGroupByNameOutId(string nameGroup, int idGroup)
        {
            try
            {
                return this.DbContext.Groups.Where(s => s.GroupName == nameGroup && s.GroupId != idGroup).ToList();
            }
            catch (Exception e)
            {
                log.Debug(e.Message);
                return null;
            }
        }
    }
}
