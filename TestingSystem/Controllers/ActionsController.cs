namespace TestingSystem.Controllers
{
    using System.Web.Mvc;
    using TestingSystem.Sevice;

    /// <summary>
    /// Defines the <see cref="ActionsController" />
    /// </summary>
    public class ActionsController : Controller
    {
        /// <summary>
        /// Defines the actionService
        /// </summary>
        public IActionService actionService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ActionsController"/> class.
        /// </summary>
        /// <param name="actionService">The actionService<see cref="IActionService"/></param>
        public ActionsController(IActionService actionService)
        {
            this.actionService = actionService;
        }

        // GET: Actions
        /// <summary>
        /// The Index
        /// </summary>
        /// <returns>The <see cref="ActionResult"/></returns>
        public ActionResult Index()
        {
            return View();
        }
    }
}
