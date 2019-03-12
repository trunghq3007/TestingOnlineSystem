namespace TestingSystem.Areas.Admin.Controllers
{
    using System.Linq;
    using System.Web.Mvc;
    using TestingSystem.BaseController;
    using TestingSystem.DataTranferObject;
    using TestingSystem.Sevice;

    /// <summary>
    /// Defines the <see cref="RoleActionController" />
    /// </summary>
    public class RoleActionController : AdminController
    {
        /// <summary>
        /// Defines the actionService
        /// </summary>
        private IActionService actionService;

        /// <summary>
        /// Defines the roleActionService
        /// </summary>
        private IRoleActionService roleActionService;

        /// <summary>
        /// Defines the roleService
        /// </summary>
        private IRoleService roleService;

        /// <summary>
        /// Initializes a new instance of the <see cref="RoleActionController"/> class.
        /// </summary>
        /// <param name="userService">The userService<see cref="IUserService"/></param>
        /// <param name="actionService">The actionService<see cref="IActionService"/></param>
        /// <param name="roleActionService">The roleActionService<see cref="IRoleActionService"/></param>
        /// <param name="roleService">The roleService<see cref="IRoleService"/></param>
        public RoleActionController(IUserService userService, IActionService actionService, IRoleActionService roleActionService, IRoleService roleService) : base(userService)
        {
            this.actionService = actionService;
            this.roleActionService = roleActionService;
            this.roleService = roleService;
        }

        // GET: Admin/RoleAction
        /// <summary>
        /// The Index
        /// </summary>
        /// <returns>The <see cref="ActionResult"/></returns>
        public ActionResult Index()
        {
            MatrixRoleAction model = new MatrixRoleAction();
            model.ListAction = actionService.GetAllActions().OrderBy(x => x.ActionName);
            model.ListRoleActions = roleActionService.GetAll().OrderBy(x => x.Action.ActionName).ToList();
            model.ListRoles = roleService.GetAll();
            return View(model);
        }

        /// <summary>
        /// The Index
        /// </summary>
        /// <param name="list">The list<see cref="string[]"/></param>
        /// <returns>The <see cref="ActionResult"/></returns>
        [HttpPost]
        public ActionResult Index(string[] list)
        {
            string[] a = list;
            if (roleActionService.Update(list))
            {
                Success = "Update permision successful!";
                return RedirectToAction("Index");
            }
            else
            {
                Failure = "Update permision fail, please try again!";
                return RedirectToAction("Index");
            }
        }
    }
}
