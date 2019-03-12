namespace TestingSystem.Areas.Admin.Controllers.Role
{
    using System.Web.Mvc;
    using TestingSystem.BaseController;
    using TestingSystem.DataTranferObject;
    using TestingSystem.Models;
    using TestingSystem.Sevice;

    /// <summary>
    /// Defines the <see cref="RoleController" />
    /// </summary>
    public class RoleController : AdminController
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
        /// Initializes a new instance of the <see cref="RoleController"/> class.
        /// </summary>
        /// <param name="userService">The userService<see cref="IUserService"/></param>
        /// <param name="actionService">The actionService<see cref="IActionService"/></param>
        /// <param name="roleActionService">The roleActionService<see cref="IRoleActionService"/></param>
        /// <param name="roleService">The roleService<see cref="IRoleService"/></param>
        public RoleController(IUserService userService, IActionService actionService, IRoleActionService roleActionService, IRoleService roleService) : base(userService)
        {
            this.actionService = actionService;
            this.roleActionService = roleActionService;
            this.roleService = roleService;
        }

        // GET: Admin/Role
        /// <summary>
        /// The Index
        /// </summary>
        /// <returns>The <see cref="ActionResult"/></returns>
        public ActionResult Index()
        {
            return View(roleService.GetAll());
        }

        /// <summary>
        /// The Delete
        /// </summary>
        /// <param name="id">The id<see cref="int"/></param>
        /// <returns>The <see cref="ActionResult"/></returns>
        public ActionResult Delete(int id)
        {
            Roles roles = roleService.GetById(id);
            roleService.DeleteRole(roles);
            return RedirectToAction("Index");
        }

        /// <summary>
        /// The Edit
        /// </summary>
        /// <param name="id">The id<see cref="int"/></param>
        /// <returns>The <see cref="ActionResult"/></returns>
        public ActionResult Edit(int id)
        {
            Roles roles = roleService.GetById(id);
            return View(roles);
        }

        /// <summary>
        /// The Edit
        /// </summary>
        /// <param name="roles">The roles<see cref="Roles"/></param>
        /// <returns>The <see cref="ActionResult"/></returns>
        [HttpPost]
        public ActionResult Edit(Roles roles)
        {
            roleService.EditRole(roles);
            return RedirectToAction("Index");
        }

        /// <summary>
        /// The Detail
        /// </summary>
        /// <param name="id">The id<see cref="int"/></param>
        /// <returns>The <see cref="ActionResult"/></returns>
        public ActionResult Detail(int id)
        {
            RoleDetail model = new RoleDetail();
            model.Role = roleService.GetById(id);
            if (roleService.GetActionInRole(id) != null)
            {
                foreach (var item in roleService.GetActionInRole(id))
                {
                    model.ListAction.Add(item);
                }
            }
            else
            {
                Models.Action action = new Models.Action();
                action.ActionName = " This role has no Action";
                model.ListAction.Add(action);
            }
            return View(model);
        }

        /// <summary>
        /// The CreateRole
        /// </summary>
        /// <returns>The <see cref="ActionResult"/></returns>
        public ActionResult CreateRole()
        {
            return View();
        }

        /// <summary>
        /// The CreateRole
        /// </summary>
        /// <param name="role">The role<see cref="Roles"/></param>
        /// <returns>The <see cref="ActionResult"/></returns>
        [HttpPost]
        public ActionResult CreateRole(Roles role)
        {
            roleService.CreateRole(role);
            return RedirectToAction("Index");
        }
    }
}
