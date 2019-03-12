namespace TestingSystem.Controllers
{
    using System.Web.Mvc;

    /// <summary>
    /// Defines the <see cref="_ErrorController" />
    /// </summary>
    public class _ErrorController : Controller
    {
        // GET: Error
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
