using System.Web.Mvc;
using System.Web.Routing;
using PPOk_Notifications.NotificationSending;
using PPOk_Notifications.Service;

namespace PPOk_Notifications
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            AspNetTimer.Start();

			ScriptService.Init();
			EmailHtmlLoader.Init();
        }
    }
}
