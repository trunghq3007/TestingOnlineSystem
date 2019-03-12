namespace TestingSystem.Areas.Admin.Controllers
{
    using System;
    using System.Web.Mvc;
    using TestingSystem.BaseController;
    using TestingSystem.Common;
    using TestingSystem.DataTranferObject;
    using TestingSystem.Models;
    using TestingSystem.Sevice;

    /// <summary>
    /// Defines the <see cref="UserController" />
    /// </summary>
    public class UserController : AdminController
    {
        /// <summary>
        /// Gets or sets the UserService
        /// </summary>
        private IUserService UserService { get; set; }

        /// <summary>
        /// Defines the RoleService
        /// </summary>
        private IRoleService RoleService;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserController"/> class.
        /// </summary>
        /// <param name="userService">The userService<see cref="IUserService"/></param>
        /// <param name="roleService">The roleService<see cref="IRoleService"/></param>
        public UserController(IUserService userService, IRoleService roleService) : base(userService)
        {
            RoleService = roleService;
            UserService = userService;
        }

        // GET: Admin/User
        /// <summary>
        /// The Index
        /// </summary>
        /// <param name="SearchString">The SearchString<see cref="string"/></param>
        /// <param name="filter">The filter<see cref="string"/></param>
        /// <param name="status">The status<see cref="string"/></param>
        /// <returns>The <see cref="ActionResult"/></returns>
        public ActionResult Index(string SearchString, string filter, string status)
        {
            ViewBag.role = RoleService.GetAll();
            if (SearchString != null)
            {
                ViewBag.SearchString = SearchString;
                var listUser = userService.ListAll(SearchString);
                ViewBag.listuser = listUser;
                return View();
            }
            if (filter != null)
            {
                var listUser = userService.Filter(filter);
                ViewBag.listuser = listUser;
                return View();
            }
            if (status != null)
            {
                if (status == "0")
                {
                    var listUser = userService.ListAllDisable();
                    ViewBag.listuser = listUser;
                    return View();
                }
                else
                {
                    var listUser = userService.ListAllActive();
                    ViewBag.listuser = listUser;
                    return View();
                }
            }
            else
            {
                var list = userService.ListAllActive();
                ViewBag.listuser = list;
                return View();
            }
        }

        // GET: Admin/User
        /// <summary>
        /// The Create
        /// </summary>
        /// <returns>The <see cref="ActionResult"/></returns>
        public ActionResult Create()
        {
            SetViewBag();
            return View();
        }

        /// <summary>
        /// The Create
        /// </summary>
        /// <param name="user">The user<see cref="User"/></param>
        /// <returns>The <see cref="ActionResult"/></returns>
        [HttpPost]
        public ActionResult Create(User user)
        {
            user.CreatedDate = DateTime.Now;
            user.UpdatedDate = DateTime.Now;
            UserService.AddUser(user);
            return RedirectToAction("Index");
        }

        /// <summary>
        /// The Delete
        /// </summary>
        /// <param name="UserId">The UserId<see cref="int"/></param>
        /// <returns>The <see cref="ActionResult"/></returns>
        public ActionResult Delete(int UserId)
        {
            if (userService.DeleteUser(UserId) > 0)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        /// <summary>
        /// The DeleteMutil
        /// </summary>
        /// <param name="list">The list<see cref="string[]"/></param>
        /// <returns>The <see cref="ActionResult"/></returns>
        [HttpPost]
        public ActionResult DeleteMutil(string[] list)
        {
            foreach (var item in list)
            {
                int id = int.Parse(item);
                userService.DeleteUser(id);
            }
            return RedirectToAction("Index");
        }

        /// <summary>
        /// The Edit
        /// </summary>
        /// <param name="UserId">The UserId<see cref="int"/></param>
        /// <returns>The <see cref="ActionResult"/></returns>
        public ActionResult Edit(int UserId)
        {
            SetViewBag();
            var user = UserService.GetUserById(UserId);
            return View(user);
        }

        /// <summary>
        /// The Edit
        /// </summary>
        /// <param name="user">The user<see cref="User"/></param>
        /// <returns>The <see cref="ActionResult"/></returns>
        [HttpPost]
        public ActionResult Edit(User user)
        {
            user.UpdatedDate = DateTime.Now;
            UserService.EditUser(user);
            return RedirectToAction("Index");
        }

        /// <summary>
        /// The SetViewBag
        /// </summary>
        /// <param name="selectedId">The selectedId<see cref="int?"/></param>
        private void SetViewBag(int? selectedId = null)
        {
            var listRole = RoleService.GetAll();
            ViewBag.RoleId = new SelectList(listRole, "RoleId", "RoleName", selectedId);
        }

        /// <summary>
        /// The IsAvailableName
        /// </summary>
        /// <param name="user">The user<see cref="User"/></param>
        /// <param name="OldUserName">The OldUserName<see cref="String"/></param>
        /// <returns>The <see cref="JsonResult"/></returns>
        [HttpPost]
        public JsonResult _IsAvailableName(User user, String OldUserName)
        {
            return Json(_IsUserAvailableName(user, OldUserName));
        }

        /// <summary>
        /// The IsUserAvailableName
        /// </summary>
        /// <param name="user">The user<see cref="User"/></param>
        /// <param name="OldUserName">The OldUserName<see cref="String"/></param>
        /// <returns>The <see cref="bool"/></returns>
        public bool _IsUserAvailableName(User user, String OldUserName)
        {
            var list = userService.ListAll();
            if (String.IsNullOrEmpty(OldUserName))
            {
                foreach (var item in list)
                    if (item.UserName == user.UserName)
                        return false;
            }
            else
            {
                foreach (var item in list)
                    if (item.UserName == user.UserName && item.UserName != OldUserName)
                        return false;
            }
            return true;
        }

        /// <summary>
        /// The IsAvailableEmail
        /// </summary>
        /// <param name="user">The user<see cref="User"/></param>
        /// <param name="OldEmail">The OldEmail<see cref="String"/></param>
        /// <returns>The <see cref="JsonResult"/></returns>
        [HttpPost]
        public JsonResult _IsAvailableEmail(User user, String OldEmail)
        {
            return Json(_IsUserAvailableEmail(user, OldEmail));
        }

        /// <summary>
        /// The IsUserAvailableEmail
        /// </summary>
        /// <param name="user">The user<see cref="User"/></param>
        /// <param name="OldEmail">The OldEmail<see cref="String"/></param>
        /// <returns>The <see cref="bool"/></returns>
        public bool _IsUserAvailableEmail(User user, String OldEmail)
        {
            var list = userService.ListAll();
            if (String.IsNullOrEmpty(OldEmail))
            {
                foreach (var item in list)
                    if (item.Email == user.Email)
                        return false;
            }
            else
            {
                foreach (var item in list)
                {
                    if (item.Email == user.Email && item.Email != OldEmail)
                        return false;
                }
            }

            return true;
        }

        /// <summary>
        /// The Detail
        /// </summary>
        /// <param name="UserId">The UserId<see cref="int"/></param>
        /// <returns>The <see cref="ActionResult"/></returns>
        public ActionResult Detail(int UserId)
        {
            UserDetail model = new UserDetail();
            Models.User user = UserService.GetUserById(UserId);
            model.Address = user.Address;
            model.CreatedDate = user.CreatedDate;
            model.Avatar = user.Avatar;
            model.Email = user.Email;
            model.Name = user.Name;
            model.Note = user.Note;
            model.Phone = user.Phone;
            model.Status = user.Status;
            model.Groups = userService.ListGroupsOfUser(user);
            return View(model);
        }
    }
}
