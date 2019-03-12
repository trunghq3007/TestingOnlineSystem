namespace TestingSystem.BaseController
{
    using System;
    using System.Collections.Generic;
    using System.Web.Mvc;
    using TestingSystem.Models;
    using TestingSystem.Sevice;

    /// <summary>
    /// Defines the <see cref="AdminController" />
    /// </summary>
    public class AdminController : Controller
    {
        /// <summary>
        /// Sets the title.
        /// </summary>
        /// <value>
        /// The title.
        /// </value>
        public string Title { set { ViewBag.Title = value; } }

        /// <summary>
        /// Sets the success.
        /// </summary>
        /// <value>The success.</value>
        public string Success { set { TempData["Success"] = ViewData["Success"] = value; } }
        /// <summary>
        /// Sets the failure.
        /// </summary>
        /// <value>The failure.</value>
        public string Failure { set { TempData["Failure"] = ViewData["Failure"] = value; } }



        protected readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        /// <summary>
        /// Defines the userService
        /// </summary>
        public IUserService userService;

        /// <summary>
        /// Initializes a new instance of the <see cref="AdminController"/> class.
        /// </summary>
        /// <param name="userService">The userService<see cref="IUserService"/></param>
        public AdminController(IUserService userService)
        {
            this.userService = userService;
        }

        /// <summary>
        /// The OnActionExecuting
        /// </summary>
        /// <param name="filterContext">The filterContext<see cref="ActionExecutingContext"/></param>
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            string controller_Action =
                filterContext.Controller.ControllerContext.RouteData.Values["controller"].ToString() + "" +
                filterContext.Controller.ControllerContext.RouteData.Values["action"].ToString();
            bool havePermision = controller_Action.Contains("_");
            if (Session["Name"] != null || havePermision)
            {
                // Get ID- Role of User
                try
                {
                    int id = int.Parse(Session["Name"].ToString());
                    User myUser = userService.GetUserById(id);
                    ViewBag.Account = myUser.Name;
                    ViewBag.Role = myUser.Roles.RoleName;

                    // Get List Action-Role true to Action
                    String Action = "";
                    List<RoleAction> myRoleActions = GetAction();
                    foreach (var item in myRoleActions)
                        Action += item.Action.ActionName + ". ";
                    ViewBag.ListActions = Action;

                    // Get Curent Action-Controller and check Permision for Action
                    if (Action.Contains(controller_Action))
                        havePermision = true;
                }
                catch(Exception e)
                {
                    log.Debug(e.Message);
                }

                if (havePermision)
                    base.OnActionExecuting(filterContext);
                else
                {
                    Response.StatusCode = 404;
                    filterContext.Result = View("Not found");
                }
            }
            else
                filterContext.Result = RedirectToAction("Index", "Home", new { Area = "" });
        }

        /// <summary>
        /// The GetAction
        /// </summary>
        /// <returns>The <see cref="List{RoleAction}"/></returns>
        protected virtual List<RoleAction> GetAction()
        {
            try
            {
                var ss = Session["Name"];
                int id = int.Parse(ss.ToString());
                return userService.GetAction(id);
            }
            catch (Exception e)
            {
                log.Debug(e.Message);
                return null;
            }
        }
    }
}
