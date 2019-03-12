namespace TestingSystem.Areas.Admin.Controllers
{
    using System;
    using System.Linq;
    using System.Net;
    using System.Web.Mvc;
    using TestingSystem.BaseController;
    using TestingSystem.DataTranferObject;
    using TestingSystem.Models;
    using TestingSystem.Sevice;

    /// <summary>
    /// Defines the <see cref="GroupController" />
    /// </summary>
    public class GroupController : AdminController
    {
        /// <summary>
        /// Defines the groupService
        /// </summary>
        private IGroupService groupService;

        /// <summary>
        /// Defines the userGroupService
        /// </summary>
        private IUserGroupService userGroupService;

        /// <summary>
        /// Initializes a new instance of the <see cref="GroupController"/> class.
        /// </summary>
        /// <param name="userService">The userService<see cref="IUserService"/></param>
        /// <param name="groupService">The groupService<see cref="IGroupService"/></param>
        /// <param name="userGroupService">The userGroupService<see cref="IUserGroupService"/></param>
        public GroupController(IUserService userService, IGroupService groupService, IUserGroupService userGroupService) : base(userService)
        {
            this.groupService = groupService;
            this.userGroupService = userGroupService;
        }

        // show list group
        /// <summary>
        /// The Index
        /// </summary>
        /// <returns>The <see cref="ActionResult"/></returns>
        public ActionResult Index()
        {
            var list = groupService.GetAllGroupC();
            return View(list);
        }

        /// <summary>
        /// The Index
        /// </summary>
        /// <param name="data">The data<see cref="GroupFilter"/></param>
        /// <returns>The <see cref="ActionResult"/></returns>
        [HttpPost]
        public ActionResult Index(GroupFilter data)
        {
            var myData = groupService.GetAllGroup(data);
            return View(myData);
        }

        // show details group
        /// <summary>
        /// The Details
        /// </summary>
        /// <param name="id">The id<see cref="int"/></param>
        /// <returns>The <see cref="ActionResult"/></returns>
        public ActionResult Details(int id)
        {
            try
            {
                if (id == 0)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Group group = groupService.GetGroupById(id);
                if (group == null)
                {
                    return HttpNotFound();
                }
                ViewBag.listUserInGroup = groupService.GetAllUserInGroup(id);
                var list = groupService.GetAllUserInGroup(id);
                ViewBag.Count = list.Count();
                return View(group);
            }
            catch (Exception e)
            {

                throw;
            }
        }

        // show form create group
        /// <summary>
        /// The Create
        /// </summary>
        /// <returns>The <see cref="ActionResult"/></returns>
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        // function to create group
        /// <summary>
        /// The Create
        /// </summary>
        /// <param name="GroupName">The GroupName<see cref="string"/></param>
        /// <param name="Description">The Description<see cref="string"/></param>
        /// <returns>The <see cref="ActionResult"/></returns>
        [HttpPost]
        public ActionResult Create(string GroupName, string Description = "")
        {
            try
            {
                var group = new Group();
                group.GroupName = GroupName;
                group.CreatedDate = DateTime.Now;
                group.UpdatedDate = DateTime.Now;
                group.Description = Description;
                groupService.CreateGroup(group);
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                throw;
            }
        }

        /// <summary>
        /// The CheckGroupNameAvailableCreate
        /// </summary>
        /// <param name="userdata">The userdata<see cref="string"/></param>
        /// <returns>The <see cref="JsonResult"/></returns>
        public JsonResult _CheckGroupNameAvailableCreate(string userdata)
        {
            try
            {
                var SeachData = groupService.GetAllGroupByName(userdata);
                if (SeachData.Count() > 0)
                {
                    return Json(1);
                }
                else
                {
                    return Json(0);
                }
            }
            catch (Exception e)
            {
                throw;
            }
        }

        /// <summary>
        /// The CheckUsernameAvailable
        /// </summary>
        /// <param name="userdata">The userdata<see cref="string"/></param>
        /// <param name="idGroup">The idGroup<see cref="int"/></param>
        /// <returns>The <see cref="JsonResult"/></returns>
        public JsonResult _CheckUsernameAvailable(string userdata, int idGroup)
        {
            try
            {
                var SeachData = groupService.GetAllGroupByNameOutId(userdata, idGroup);
                if (SeachData.Count() > 0)
                {
                    return Json(1);
                }
                else
                {
                    return Json(0);
                }
            }
            catch (Exception e)
            {
                throw;
            }
        }

        /// <summary>
        /// The CheckDeleteAvailable
        /// </summary>
        /// <param name="idGroup">The idGroup<see cref="int"/></param>
        /// <returns>The <see cref="JsonResult"/></returns>
        public JsonResult _CheckDeleteAvailable(int idGroup)
        {
            try
            {
                var SeachData = groupService.GetAllUserInGroup(idGroup);
                if (SeachData.Count() > 0)
                {
                    return Json(1);
                }
                else
                {
                    return Json(0);
                }
            }
            catch (Exception e)
            {
                throw;
            }
        }

        // check if data exists in database
        /// <summary>
        /// The IsAvailable
        /// </summary>
        /// <param name="GroupName">The GroupName<see cref="string"/></param>
        /// <returns>The <see cref="JsonResult"/></returns>
        [HttpPost]
        public JsonResult _IsAvailable(string GroupName)
        {
            return Json(_IsUnique(GroupName));
        }

        /// <summary>
        /// The IsUnique
        /// </summary>
        /// <param name="GroupName">The GroupName<see cref="string"/></param>
        /// <returns>The <see cref="bool"/></returns>
        public bool _IsUnique(string GroupName)
        {
            var list = groupService.GetAllGroupC();
            foreach (var item in list)
            {
                if (item.GroupName == GroupName)
                {
                    return false;
                }
            }
            return true;
        }

        // function to delete group by idgroup
        /// <summary>
        /// The Delete
        /// </summary>
        /// <param name="id">The id<see cref="int"/></param>
        /// <returns>The <see cref="ActionResult"/></returns>
        public ActionResult Delete(int id)
        {
            int checkDel = groupService.DeleteGroup(id);
            return RedirectToAction("Index");
        }

        // GET: Groups/Edit/5
        // show form edit group
        /// <summary>
        /// The Edit
        /// </summary>
        /// <param name="id">The id<see cref="int"/></param>
        /// <returns>The <see cref="ActionResult"/></returns>
        public ActionResult Edit(int id)
        {
            try
            {
                if (id == 0)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Group group = groupService.GetGroupById(id);
                if (group == null)
                {
                    return HttpNotFound();
                }
                return View(group);
            }
            catch (Exception e)
            {
                throw;
            }
        }


        /// <summary>
        /// The Edit
        /// </summary>
        /// <param name="CreatedDate">The CreatedDate<see cref="DateTime"/></param>
        /// <param name="GroupId">The GroupId<see cref="int"/></param>
        /// <param name="GroupName">The GroupName<see cref="string"/></param>
        /// <param name="Description">The Description<see cref="string"/></param>
        /// <returns>The <see cref="ActionResult"/></returns>
        [HttpPost]
        // function to edit group
        public ActionResult Edit(DateTime CreatedDate, int GroupId, string GroupName, string Description = "")
        {
            try
            {
                var group = new Group();
                group.GroupId = GroupId;
                group.GroupName = GroupName;
                group.CreatedDate = CreatedDate;
                group.UpdatedDate = DateTime.Now;
                group.Description = Description;
                groupService.EditGroup(group);
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {

                throw;
            }
        }

        // function to Search group by NameGroup
        /// <summary>
        /// The Search
        /// </summary>
        /// <param name="nameGroup">The nameGroup<see cref="string"/></param>
        /// <returns>The <see cref="ActionResult"/></returns>
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Search(string nameGroup)
        {
            var list = groupService.GetAllGroupByName(nameGroup);
            return View(list);
        }

        // function to show update account in group
        /// <summary>
        /// The UpdateAccount
        /// </summary>
        /// <param name="id">The id<see cref="int"/></param>
        /// <returns>The <see cref="ActionResult"/></returns>
        public ActionResult UpdateAccount(int id)
        {
            try
            {
                // get all user
                ViewBag.ListUser = userService.ListAllActive();
                // get all usergroup
                ViewBag.ListUserGroup = userGroupService.GetAllUserGroupsByIdGroup(id);
                ViewBag.IdGroup = id;
                var countListUserGroup = userGroupService.GetAllUserGroupsByIdGroup(id).Count();
                ViewBag.CheckUser = false;
                ViewBag.CountListGroup = countListUserGroup;
                return View();
            }
            catch (ArgumentNullException e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// The UpdateAccount
        /// </summary>
        /// <param name="id">The id<see cref="int"/></param>
        /// <param name="SearchString">The SearchString<see cref="string"/></param>
        /// <returns>The <see cref="ActionResult"/></returns>
        [HttpPost]
        public ActionResult UpdateAccount(int id, string SearchString)
        {
            try
            {
                // get all user by name
                ViewBag.ListUser = userService.ListAll(SearchString);
                // get all usergroup
                ViewBag.ListUserGroup = userGroupService.GetAllUserGroupsByIdGroup(id);
                ViewBag.IdGroup = id;
                var countListUserGroup = userGroupService.GetAllUserGroupsByIdGroup(id).Count();
                ViewBag.CheckUser = false;
                ViewBag.CountListGroup = countListUserGroup;
                return View();
            }
            catch (ArgumentNullException e)
            {
                throw new Exception(e.Message);
            }
        }

        // function to add account into group
        /// <summary>
        /// The AddAccount
        /// </summary>
        /// <param name="idUser">The idUser<see cref="int"/></param>
        /// <param name="idGroup">The idGroup<see cref="int"/></param>
        /// <returns>The <see cref="ActionResult"/></returns>
        public ActionResult AddAccount(int idUser, int idGroup)
        {
            userGroupService.CreateUserGroup(idUser, idGroup, true);
            return RedirectToAction("UpdateAccount", "Group", new { id = idGroup });
        }

        // function to remove account from group
        /// <summary>
        /// The RemoveAccount
        /// </summary>
        /// <param name="idUser">The idUser<see cref="int"/></param>
        /// <param name="idGroup">The idGroup<see cref="int"/></param>
        /// <returns>The <see cref="ActionResult"/></returns>
        public ActionResult RemoveAccount(int idUser, int idGroup)
        {
            userGroupService.DeleteUserGroup(idUser, idGroup, true);
            return RedirectToAction("UpdateAccount", "Group", new { id = idGroup });
        }

        /// <summary>
        /// The DeleteMutil
        /// </summary>
        /// <param name="list">The list<see cref="string[]"/></param>
        /// <returns>The <see cref="ActionResult"/></returns>
        [HttpPost]
        public ActionResult DeleteMutil(string[] list)
        {
            try
            {
                var listGroup = groupService.GetAllGroupC();
                if (listGroup != null)
                {
                    if (listGroup.Count() > 0)
                    {
                        foreach (var item in list)
                        {
                            int id = int.Parse(item);
                            groupService.DeleteGroup(id);
                        }
                    }
                }
                return RedirectToAction("Index", new { groupFilter = listGroup });
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
