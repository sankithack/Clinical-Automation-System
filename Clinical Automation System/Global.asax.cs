using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Clinical_Automation_System
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Session_Start()
        {
            Session["UserId"] = 0;
            Session["Name"] = "";
            Session["RoleId"] = 0;
            Session["Invalid"] = false;
        }
    }
}
