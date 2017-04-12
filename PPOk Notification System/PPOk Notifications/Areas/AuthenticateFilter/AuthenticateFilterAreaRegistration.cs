using System.Web.Mvc;

namespace PPOk_Notifications.Areas.AuthenticateFilter
{
    public class AuthenticateFilterAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "AuthenticateFilter";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "AuthenticateFilter_default",
                "AuthenticateFilter/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}