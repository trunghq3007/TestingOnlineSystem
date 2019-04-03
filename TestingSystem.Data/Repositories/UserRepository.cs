using Microsoft.SqlServer.Server;

namespace TestingSystem.Data.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.IO;
    using System.Linq;
    using System.Web;
    using System.Xml;
    using TestingSystem.Common;
    using TestingSystem.Data.Infrastructure;
    using TestingSystem.DataTranferObject;
    using TestingSystem.Models;

    /// <summary>
    /// Defines the <see cref="IUserRepository" />
    /// </summary>
    public interface IUserRepository : IRepository<User>
    {
        /// <summary>
        /// The Login
        /// </summary>
        /// <param name="user">The user<see cref="UserLogin"/></param>
        /// <returns>The <see cref="int"/></returns>
        int Login(UserLogin user);

        /// <summary>
        /// The Active
        /// </summary>
        /// <param name="key">The key<see cref="string"/></param>
        /// <returns>The <see cref="int"/></returns>
        int Active(string key);

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
        /// The ListAllActive
        /// </summary>
        /// <returns>The <see cref="List{User}"/></returns>
        List<User> ListAllActive();

        /// <summary>
        /// The DeleteUser
        /// </summary>
        /// <param name="userId">The userId<see cref="int"/></param>
        /// <returns>The <see cref="int"/></returns>
        int DeleteUser(int userId);

        /// <summary>
        /// The GetUserById
        /// </summary>
        /// <param name="UserId">The UserId<see cref="int"/></param>
        /// <returns>The <see cref="User"/></returns>
        User GetUserById(int UserId);

        /// <summary>
        /// The EditUser
        /// </summary>
        /// <param name="user">The user<see cref="User"/></param>
        /// <returns>The <see cref="int"/></returns>
        int EditUser(User user);

        /// <summary>
        /// The ListAll
        /// </summary>
        /// <returns>The <see cref="List{User}"/></returns>
        List<User> ListAll();

        /// <summary>
        /// The SearchUser
        /// </summary>
        /// <param name="SearchString">The SearchString<see cref="string"/></param>
        /// <returns>The <see cref="List{User}"/></returns>
        List<User> SearchUser(string SearchString);

        /// <summary>
        /// The ListGroups
        /// </summary>
        /// <param name="user">The user<see cref="User"/></param>
        /// <returns>The <see cref="List{Group}"/></returns>
        List<Group> ListGroups(User user);

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
    /// Defines the <see cref="UserRepository" />
    /// </summary>
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        /// <summary>
        /// Defines the log
        /// </summary>
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Initializes a new instance of the <see cref="UserRepository"/> class.
        /// </summary>
        /// <param name="dbFactory">The dbFactory<see cref="IDbFactory"/></param>
        public UserRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

        /// <summary>
        /// The Login
        /// </summary>
        /// <param name="user">The user<see cref="UserLogin"/></param>
        /// <returns>The <see cref="int"/></returns>
        public int Login(UserLogin user)
        {
            var password = Encryptor.MD5Hash(user.password);
            User myUser = (from s in DbContext.Users
                           where s.UserName == user.userName
                           && s.Password == password
                           select s).FirstOrDefault();
            if (myUser != null)
            {
                if (myUser.Status == 0) return 0;
                return myUser.UserId;
            }
            return -1;
        }

        /// <summary>
        /// The GetAction
        /// </summary>
        /// <param name="userId">The userId<see cref="int"/></param>
        /// <returns>The <see cref="List{RoleAction}"/></returns>
        public List<RoleAction> GetAction(int userId)
        {
            //User myUser = GetById(userId);
            try
            {
                var a = (from s in DbContext.Users
                         join s2 in DbContext.Roles
                             on s.RoleId equals s2.RoleId
                         where s.UserId == userId
                         select s).FirstOrDefault();
                var myActions = from s1 in DbContext.RoleActions
                                join s2 in DbContext.Roles
                                    on s1.RoleId equals s2.RoleId
                                where s1.RoleId == a.RoleId && s1.IsTrue
                                select s1;
                return myActions.ToList();
            }
            catch (Exception e)
            {
                log.Debug(e.Message);
                return null;
            }
        }

        /// <summary>
        /// The GetRoleId
        /// </summary>
        /// <param name="userId">The userId<see cref="int"/></param>
        /// <returns>The <see cref="int"/></returns>
        public int GetRoleId(int userId)
        {
            try
            {
                User myUser = GetById(userId);
                return myUser.RoleId;
            }
            catch (Exception e)
            {
                log.Debug(e.Message);
                return 0;
            }
        }
        public User GetUserName(int Id)
        {
            return DbContext.Users.Where(x => x.UserId == Id).SingleOrDefault();
        }

        /// <summary>
        /// The Register
        /// </summary>
        /// <param name="user">The user<see cref="UserRegister"/></param>
        /// <returns>The <see cref="bool"/></returns>
        public bool Register(UserRegister user)
        {
            try
            {
                var Account = DbContext.Users;
                User newUser = new User();
                newUser.Address = "";
                newUser.Avatar = "";
                newUser.CreatedDate = DateTime.Now;
                newUser.UpdatedDate = DateTime.Now;
                newUser.Note = "";
                newUser.RoleId = 3;
                newUser.Status = 0;
                newUser.Email = user.email;
                newUser.Phone = "";
                newUser.Name = user.fullname;
                newUser.UserName = user.username;
                newUser.Password = user.password;
                Account.Add(newUser);
                if (DbContext.SaveChanges() == 1)
                {
                    Send(newUser.Email, newUser.Name);
                    return true;
                }
                else
                    return false;
            }
            catch (Exception e)
            {
                log.Debug(e.Message);
                return false;
            }
        }

        /// <summary>
        /// The Send
        /// </summary>
        /// <param name="emailTo">The emailTo<see cref="string"/></param>
        /// <param name="name">The name<see cref="string"/></param>
        private void Send(string emailTo, string name)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(HttpContext.Current.Server.MapPath("~/TempEmail/Config/Data.xml"));

            string domain = doc.DocumentElement.SelectSingleNode("/root/domain").InnerText;
            string Email = doc.DocumentElement.SelectSingleNode("/root/email/username").InnerText;
            string Pass = doc.DocumentElement.SelectSingleNode("/root/email/pass").InnerText;
            string SendAs = doc.DocumentElement.SelectSingleNode("/root/email/sendAs").InnerText;
            string NameAs = doc.DocumentElement.SelectSingleNode("/root/email/name").InnerText;
            string Smtp = doc.DocumentElement.SelectSingleNode("/root/email/smtp").InnerText;
            string Port = doc.DocumentElement.SelectSingleNode("/root/email/port").InnerText;

            String EmailTo = emailTo;
            SendEmail mySendEmail = new SendEmail(Email, Pass, SendAs, NameAs, Smtp, Port);
            String Subject = "Xác thực tài khoản!";
            String Body = "";
            StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath("~/TempEmail/Verify-mail.html"));
            string r = "";
            while ((r = reader.ReadLine()) != null)
            {
                if (r.Trim() == "[Name]")
                    r = " " + name;
                if (r.Trim() == "[Verify-link]")
                    r = domain + "/Account/Verify/?key=" + Base64.Encode(EmailTo);
                Body += r;
            }
            string results = mySendEmail.Send(EmailTo, Subject, Body);
            if (results != "true")
                log.Debug(results);
        }

        /// <summary>
        /// The Active
        /// </summary>
        /// <param name="key">The key<see cref="string"/></param>
        /// <returns>The <see cref="int"/></returns>
        public int Active(string key)
        {
            User myUser = (from s in DbContext.Users
                           where s.Email == key
                           select s).FirstOrDefault();
            if (myUser != null)
            {
                if (myUser.Status == 0)
                {
                    myUser.Status = 1;
                    return DbContext.SaveChanges();
                }

                return myUser.Status;
            }
            return -1;
        }

        /// <summary>
        /// The AddUser
        /// </summary>
        /// <param name="entity">The entity<see cref="User"/></param>
        /// <returns>The <see cref="int"/></returns>
        public int AddUser(User entity)
        {
            try
            {

                var Password = Encryptor.MD5Hash(entity.Password);
                entity.Password = Password;
                DbContext.Users.Add(entity);
                return DbContext.SaveChanges();
            }
            catch (Exception e)
            {
                log.Debug(e.Message);
                return 0;
            }
        }

        /// <summary>
        /// The ListAllActive
        /// </summary>
        /// <returns>The <see cref="List{User}"/></returns>
        public List<User> ListAllActive()
        {
            try
            {
                var list = DbContext.Users.Where(c => c.Status == 1);
                List<User> listusers = list.ToList();
                return listusers;
            }
            catch (Exception e)
            {
                log.Debug(e.Message);
                return null;
            }
        }

        /// <summary>
        /// The DeleteUser
        /// </summary>
        /// <param name="userId">The userId<see cref="int"/></param>
        /// <returns>The <see cref="int"/></returns>
        public int DeleteUser(int userId)
        {
            try
            {
                var user = DbContext.Users.Find(userId);
                user.Status = 0;
                DbContext.Entry(user).State = EntityState.Modified;
                return DbContext.SaveChanges();
            }
            catch (Exception e)
            {
                log.Debug(e.Message);
                return 0;
            }
        }

        /// <summary>
        /// The GetUserById
        /// </summary>
        /// <param name="UserId">The UserId<see cref="int"/></param>
        /// <returns>The <see cref="User"/></returns>
        public User GetUserById(int UserId)
        {
            try
            {
                return DbContext.Users.Find(UserId);
            }
            catch (Exception e)
            {
                log.Debug(e.Message);
                return null;
            }
        }

        /// <summary>
        /// The EditUser
        /// </summary>
        /// <param name="user">The user<see cref="User"/></param>
        /// <returns>The <see cref="int"/></returns>
        public int EditUser(User user)
        {
            try
            {
                User myUser = GetUserById(user.UserId);
                //DateTime? createData = myUser.CreatedDate;
                myUser.Address = user.Address;
                myUser.Note = user.Note;
                myUser.RoleId = user.RoleId;
                myUser.Email = user.Email;
                myUser.Name = user.Name;
                myUser.Phone = user.Phone;
                myUser.UpdatedDate = DateTime.Now;
                myUser.Status = user.Status;
                return DbContext.SaveChanges();
            }
            catch (Exception e)
            {
                log.Debug(e.Message);
                return 0;
            }
        }

        /// <summary>
        /// The ListAll
        /// </summary>
        /// <returns>The <see cref="List{User}"/></returns>
        public List<User> ListAll()
        {
            try
            {
                var list = DbContext.Users.ToList();
                return list;
            }
            catch (Exception e)
            {
                log.Debug(e.Message);
                return null;
            }
        }

        /// <summary>
        /// The SearchUser
        /// </summary>
        /// <param name="SearchString">The SearchString<see cref="string"/></param>
        /// <returns>The <see cref="List{User}"/></returns>
        public List<User> SearchUser(string SearchString)
        {
            var listUser = ListAllActive();
            if (SearchString != null)
            {
                try
                {
                    var list = listUser.Where(x => x.UserName.Contains(SearchString) || x.Name.Contains(SearchString) || x.Email.Contains(SearchString)).ToList();
                    return list;
                }
                catch (Exception e)
                {
                    log.Debug(e.Message);
                    return null;
                }
            }
            return null;
        }

        /// <summary>
        /// The ListGroups
        /// </summary>
        /// <param name="user">The user<see cref="User"/></param>
        /// <returns>The <see cref="List{Group}"/></returns>
        public List<Group> ListGroups(User user)
        {
            try
            {
                List<Group> result = new List<Group>();
                result = (from g in DbContext.Groups
                          join ug in DbContext.UserGroups on g.GroupId equals ug.GroupId
                          where ug.UserId == user.UserId
                          select g).ToList();
                return result;
            }
            catch (Exception e)
            {
                log.Debug(e.Message);
                return null;
            }
        }

        /// <summary>
        /// The Filter
        /// </summary>
        /// <param name="filterstring">The filterstring<see cref="string"/></param>
        /// <returns>The <see cref="List{User}"/></returns>
        public List<User> Filter(string filterstring)
        {
            try
            {
                var listUser = ListAllActive();
                if (filterstring != null)
                {
                    var list = listUser.Where(x => x.RoleId.ToString().Contains(filterstring)).ToList();
                    return list;
                }

                return null;
            }
            catch (Exception e)
            {

                log.Debug(e.Message);
                return null;
            }
        }

        /// <summary>
        /// The CountUser
        /// </summary>
        /// <returns>The <see cref="int"/></returns>
        public int CountUser()
        {
            try
            {
                return DbContext.Users.Count();
            }
            catch (Exception e)
            {
                log.Debug(e.Message);
                return 0;
            }
        }

        /// <summary>
        /// The ListAllDisable
        /// </summary>
        /// <returns>The <see cref="List{User}"/></returns>
        public List<User> ListAllDisable()
        {
            try
            {
                var list = DbContext.Users.Where(c => c.Status == 0);
                List<User> listusers = list.ToList();
                return listusers;
            }
            catch (Exception e)
            {
                log.Debug(e.Message);
                return null;
            }
        }

        public bool Recovery(string email)
        {
            try
            {
                string email1 = email + "_" + DateTime.Now.AddMinutes(5).ToString();
                String hash = "1" + Base64.Encode(email1);
                List<char> hashList = hash.ToCharArray().ToList();
                hashList.Reverse();
                hash = "";
                foreach (var item in hashList)
                    hash += item;
                SendRecovery(email, "", hash);
                return true;
            }
            catch (Exception e)
            {
                log.Debug(e.Message);
                return false;
            }
        }

        private void SendRecovery(string emailTo, string name, string key)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(HttpContext.Current.Server.MapPath("~/TempEmail/Config/Data.xml"));

            string domain = doc.DocumentElement.SelectSingleNode("/root/domain").InnerText;
            string Email = doc.DocumentElement.SelectSingleNode("/root/email/username").InnerText;
            string Pass = doc.DocumentElement.SelectSingleNode("/root/email/pass").InnerText;
            string SendAs = doc.DocumentElement.SelectSingleNode("/root/email/sendAs").InnerText;
            string NameAs = doc.DocumentElement.SelectSingleNode("/root/email/name").InnerText;
            string Smtp = doc.DocumentElement.SelectSingleNode("/root/email/smtp").InnerText;
            string Port = doc.DocumentElement.SelectSingleNode("/root/email/port").InnerText;

            String EmailTo = emailTo;
            SendEmail mySendEmail = new SendEmail(Email, Pass, SendAs, NameAs, Smtp, Port);
            String Subject = "Online testing system - khôi phục mật khẩu!";
            String Body = "";
            StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath("~/TempEmail/Recovery-mail.html"));
            string r = "";
            while ((r = reader.ReadLine()) != null)
            {
                if (r.Trim() == "[Name]")
                    r = " " + name;
                if (r.Trim() == "[Recovery-link]")
                    r = domain + "/Account/Reset/?key=" + key;
                Body += r;
            }
            string results = mySendEmail.Send(EmailTo, Subject, Body);
            if (results != "true")
                log.Debug(results);
        }

        public bool Reset(string email, string pass)
        {
            try
            {
                var myUser = DbContext.Users.Where(x => x.Email == email).FirstOrDefault();
                myUser.Password = pass;
                return DbContext.SaveChanges()==1;
            }
            catch (Exception e)
            {
                log.Debug(e.Message);
                return false;
            }
        }

        public List<User> GetAllUserRoleIsMemberOrSubMember()
        {
	        var listUser = DbContext.Users.Where(x => x.RoleId == 3 || x.RoleId == 4||x.RoleId==2).ToList();
	        return listUser;
        }

        public List<User> GetAllUserRoleIsMemberOrSubMemberByKeySearch(string keySearch)
        {
	        var listUser = GetAllUserRoleIsMemberOrSubMember().Where(x=>x.Name.Contains(keySearch)||x.UserName.Contains(keySearch)).ToList();
	        return listUser;
        }
    }
}
