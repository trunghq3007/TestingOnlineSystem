namespace TestingSystem.Areas.Admin.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Web.Mvc;
    using TestingSystem.BaseController;
    using TestingSystem.Sevice;
    using Action = TestingSystem.Models.Action;

    /// <summary>
    /// Defines the <see cref="ActionController" />
    /// </summary>
    public class ActionController : AdminController
    {
        /// <summary>
        /// Defines the actionService
        /// </summary>
        private IActionService actionService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ActionController"/> class.
        /// </summary>
        /// <param name="userService">The userService<see cref="IUserService"/></param>
        /// <param name="actionService">The actionService<see cref="IActionService"/></param>
        public ActionController(IUserService userService, IActionService actionService) : base(userService)
        {
            this.actionService = actionService;
        }

        // GET: Admin/Action
        /// <summary>
        /// The Index
        /// </summary>
        /// <param name="key">The key<see cref="string"/></param>
        /// <returns>The <see cref="ActionResult"/></returns>
        public ActionResult Index(string key)
        {
            var list = new List<Action>();
            if (String.IsNullOrEmpty(key))
                list = actionService.GetAllActions().ToList();
            else
            {
                list = actionService.Search(key).ToList();
                @ViewBag.Key = key;
            }
            ViewData["ListAction"] = list;
            return View();
        }

        /// <summary>
        /// The Scan
        /// </summary>
        /// <returns>The <see cref="ActionResult"/></returns>
        public ActionResult Scan()
        {
            List<string> myNewList = GetAll();
            if (actionService.ScanUpdate(myNewList))
            {
                Success = "Scan success!";
                return RedirectToAction("Index");
            }
            else
            {
                Failure = "Scan fail";
                return RedirectToAction("Index");
            }
        }

        //public ActionResult Create()
        //{
        //    return View();
        //}
        //[HttpPost]
        //public ActionResult Create(Action myAction)
        //{
        //    actionService.CreateAction(myAction);
        //    return RedirectToAction("Index");
        //}
        /// <summary>
        /// The IsAvailable
        /// </summary>
        /// <param name="ActionName">The ActionName<see cref="string"/></param>
        /// <param name="oldName">The oldName<see cref="string"/></param>
        /// <returns>The <see cref="JsonResult"/></returns>
        [HttpPost]
        public JsonResult _IsAvailable(string ActionName, string oldName)
        {
            return Json(_IsGroupAvailable(ActionName, oldName));
        }

        /// <summary>
        /// The IsGroupAvailable
        /// </summary>
        /// <param name="ActionName">The ActionName<see cref="string"/></param>
        /// <param name="oldName">The oldName<see cref="string"/></param>
        /// <returns>The <see cref="bool"/></returns>
        private bool _IsGroupAvailable(string ActionName, string oldName)
        {
            var list = actionService.GetAllActions();
            if (String.IsNullOrEmpty(oldName))
            {
                foreach (var item in list)
                    if (item.ActionName == ActionName)
                        return false;
            }
            else
            {
                foreach (var item in list)
                    if (item.ActionName == ActionName && item.ActionName != oldName)
                        return false;
            }
            return true;
        }

        //[HttpGet]
        /// <summary>
        /// The Delete
        /// </summary>
        /// <param name="id">The id<see cref="int"/></param>
        /// <returns>The <see cref="ActionResult"/></returns>
        public ActionResult Delete(int id)
        {
            actionService.DeleteAction(id);
            return RedirectToAction("Index");
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
                if (list == null) return RedirectToAction("Index");
                foreach (string item in list)
                {
                    int i = int.Parse(item);
                    actionService.DeleteAction(i);
                }
                Success="Delete success";
            }
            catch(Exception e)
            {
                log.Debug(e.Message);
                Failure = "Delete mutil fail...";
            }
            return RedirectToAction("Index");
        }

        /// <summary>
        /// The Detail
        /// </summary>
        /// <param name="id">The id<see cref="int"/></param>
        /// <returns>The <see cref="ActionResult"/></returns>
        public ActionResult Detail(int id)
        {
            var myAction = actionService.Get(id);
            ViewData["Action"] = myAction;
            ViewData["RoleList"] = actionService.GetRoleses(id);
            return View();
        }

        /// <summary>
        /// The Edit
        /// </summary>
        /// <param name="id">The id<see cref="int"/></param>
        /// <returns>The <see cref="ActionResult"/></returns>
        public ActionResult Edit(int id)
        {
            string currentActionName = ControllerContext.RouteData.GetRequiredString("action");
            ViewBag.Action = currentActionName;
            var myAction = actionService.Get(id);
            return View(myAction);
        }

        /// <summary>
        /// The Edit
        /// </summary>
        /// <param name="myAction">The myAction<see cref="Action"/></param>
        /// <returns>The <see cref="ActionResult"/></returns>
        [HttpPost]
        public ActionResult Edit(Action myAction)
        {
            if (actionService.EditAction(myAction))
            {
                Success = "Update success!";
                return RedirectToAction("Index");
            }
            Failure = "Update fail! please try again!";
            return RedirectToAction("Edit/"+myAction.ActionId);
        }

        /// <summary>
        /// The GetAll
        /// </summary>
        /// <returns>The <see cref="List{string}"/></returns>
        private List<string> GetAll()
        {
            Assembly asm = Assembly.GetExecutingAssembly();
            var controlleractionlist = asm.GetTypes()
                .Where(type => typeof(Controller).IsAssignableFrom(type))
                .SelectMany(type => type.GetMethods(BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.Public))
                .Where(m => !m.GetCustomAttributes(typeof(System.Runtime.CompilerServices.CompilerGeneratedAttribute), true).Any())
                .Select(x => new { Controller = x.DeclaringType.Name, Action = x.Name })
                .OrderBy(x => x.Controller).ThenBy(x => x.Action).ToList();

            List<string> ListAction = new List<string>();
            foreach (var item in controlleractionlist)
            {
                string temp = item.Controller + item.Action;
                temp = temp.Replace("Controller", "");
                if (!ListAction.Contains(temp)
                    && !temp.Contains("_")
                )
                    ListAction.Add(temp);
            }
            return ListAction;
        }
    }
}
