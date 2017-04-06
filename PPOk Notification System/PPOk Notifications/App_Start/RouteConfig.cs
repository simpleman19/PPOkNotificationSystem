using System.Web.Mvc;
using System.Web.Routing;

namespace PPOk_Notifications
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
			routes.MapRoute(
				name: "ResetReset",
				url: "Email/Reset/{userid}/{otp}",
				defaults: new { controller="Email", action="Reset" }
			);
			routes.MapRoute(
				name: "EmailRespond",
				url: "Email/Respond/{patientid}/{otp}",
				defaults: new { controller="Email", action="Respond" }
			);
			routes.MapRoute(
				name: "EmailUnsubscribe",
				url: "Email/Unsubscribe/{patientid}/{otp}",
				defaults: new { controller = "Email", action = "Unsubscribe" }
			);
			routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Login", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
