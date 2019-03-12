namespace TestingSystem.Controllers
{
    using System.Web.Mvc;
    using TestingSystem.Common;
    using TestingSystem.DataTranferObject;
    using TestingSystem.Sevice;

    /// <summary>
    /// Defines the <see cref="_RegisterController" />
    /// </summary>
    public class _RegisterController : Controller
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
        /// Initializes a new instance of the <see cref="_RegisterController"/> class.
        /// </summary>
        /// <param name="userService">The userService<see cref="IUserService"/></param>
        public _RegisterController(IUserService userService)
        {
            this.userService = userService;
        }

        //public RegisterController(IUserService userService) : base(userService)
        //{
        //}

        // GET: Register
        /// <summary>
        /// The Index
        /// </summary>
        /// <returns>The <see cref="ActionResult"/></returns>
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// The Index
        /// </summary>
        /// <param name="user">The user<see cref="UserRegister"/></param>
        /// <returns>The <see cref="ActionResult"/></returns>
        [HttpPost]
        public ActionResult Index(UserRegister user)
        {
            if (user.password != user.comfirmPassword)
            {
                Failure = "Your comfirm password not same password";
                return View();
            }
            user.password = Encryptor.MD5Hash(user.password);
            if (userService.Register(user))
            {
                Success = "Register success, sign in";
                return RedirectToAction("Login", "Login");
            }
            else
            {
                Failure = "Register fail, please try again.";
                return View();
            }
            
        }
    }
}
