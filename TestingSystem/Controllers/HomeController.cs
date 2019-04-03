namespace TestingSystem.Controllers
{
    using System.Web.Mvc;
    using TestingSystem.BaseController;
    using TestingSystem.Sevice;

    /// <summary>
    /// Defines the <see cref="HomeController" />
    /// </summary>
    public class HomeController : Controller
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HomeController"/> class.
        /// </summary>
        /// <param name="userService">The userService<see cref="IUserService"/></param>
        /// 
        private readonly IUserService userService;
        public HomeController(IUserService userService) 
        {
            this.userService = userService;
        }
        /// <summary>
        /// The Index
        /// </summary>
        /// <returns>The <see cref="ActionResult"/></returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// The About
        /// </summary>
        /// <returns>The <see cref="ActionResult"/></returns>
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        /// <summary>
        /// The Contact
        /// </summary>
        /// <returns>The <see cref="ActionResult"/></returns>
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
