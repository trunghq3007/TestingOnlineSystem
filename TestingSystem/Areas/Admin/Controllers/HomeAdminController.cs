namespace TestingSystem.Areas.Admin.Controllers
{
    using System.Web.Mvc;
    using TestingSystem.BaseController;
    using TestingSystem.Sevice;

    /// <summary>
    /// Defines the <see cref="HomeAdminController" />
    /// </summary>
    public class HomeAdminController : AdminController
    {
        /// <summary>
        /// Defines the UserService
        /// </summary>
        private IUserService UserService;

        /// <summary>
        /// Initializes a new instance of the <see cref="HomeAdminController"/> class.
        /// </summary>
        /// <param name="userService">The userService<see cref="IUserService"/></param>
        public HomeAdminController(IUserService userService) : base(userService)
        {
            this.UserService = userService;
        }

        // GET: Admin/Home
        /// <summary>
        /// The Index
        /// </summary>
        /// <returns>The <see cref="ActionResult"/></returns>
        public ActionResult Index()
        {
            ViewBag.CountUser = UserService.CountUser();
            return View();
        }
    }
}
