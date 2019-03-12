using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using TestingSystem.App_Start;

namespace TestingSystem
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            //
            DatabaseSetup.Initialize();
            // Autofac and Automapper configurations
            log4net.Config.XmlConfigurator.Configure();
            Bootstrapper.Run();
        }
    }
}
