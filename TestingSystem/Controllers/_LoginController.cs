namespace TestingSystem.Areas.Admin.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Web.Mvc;
    using TestingSystem.DataTranferObject;
    using TestingSystem.Models;
    using TestingSystem.Sevice;

    /// <summary>
    /// Defines the <see cref="LoginController" />
    /// </summary>
    public class _LoginController : Controller
    {

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

        /// <summary>
        /// Defines the userService
        /// </summary>
        protected IUserService userService;

        /// <summary>
        /// Initializes a new instance of the <see cref="LoginController"/> class.
        /// </summary>
        /// <param name="userService">The userService<see cref="IUserService"/></param>
        public _LoginController(IUserService userService)
        {
            this.userService = userService;
        }

        /// <summary>
        /// The OnActionExecuting
        /// </summary>
        /// <param name="filterContext">The filterContext<see cref="ActionExecutingContext"/></param>
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (Session["Name"] != null)
            {
                filterContext.Result = RedirectToAction("Index", "Home");
                return;
            }
            base.OnActionExecuting(filterContext);
        }

        // GET: Admin/Login
        /// <summary>
        /// The Login
        /// </summary>
        /// <returns>The <see cref="ActionResult"/></returns>
        public ActionResult Login()
        {
            return View();
        }

        /// <summary>
        /// The Login
        /// </summary>
        /// <param name="user">The user<see cref="UserLogin"/></param>
        /// <returns>The <see cref="ActionResult"/></returns>
        [HttpPost]
        public ActionResult Login(UserLogin user)
        {
            if (String.IsNullOrEmpty(user.password))
                return RedirectToAction("Login");
            int id = userService.Login(user);
            if (id > 0)
            {
                //Success = "Sign in success";
                bool AdminPage = false;
                Session.Add("Name", id);
                List<RoleAction> myRoleActions = GetAction();
                foreach (var item in myRoleActions)
                {
                    if (item.Action.ActionName == "LoginAdminLayout")
                        AdminPage = true;
                }
                if (AdminPage)
                    return RedirectToAction("Index", "HomeAdmin", new { Area = "Admin" });
                else
                    return RedirectToAction("Index", "Home");
            }
            else
            {
                if (id == 0)
                {
                    Failure = "Your account need comfirm email to Sign in";
                    return View();
                }
                else
                {
                    Failure = "Your user name or password is not correct!";
                    return View();
                }
            }
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
                return null;
            }
        }
    }
}
