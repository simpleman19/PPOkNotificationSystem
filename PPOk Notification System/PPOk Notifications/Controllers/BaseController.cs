using System.Diagnostics.Contracts;
using System.Linq;
using System.Web.Mvc;

namespace PPOk_Notifications.Controllers
{
    public abstract class BaseController : Controller
    {
        protected override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (SkipAuthorization(filterContext))
            {
                return;
            }

            if (filterContext.HttpContext.Session == null || filterContext.HttpContext.Session["user-id"] == null)
            {
                filterContext.Result = Redirect("/Login/Index");
            }
        }

        private static bool SkipAuthorization(AuthorizationContext filterContext)
        {
            Contract.Assert(filterContext != null);

            return filterContext.ActionDescriptor.GetCustomAttributes(typeof(AllowAnonymousAttribute), true).Any()
                   || filterContext.ActionDescriptor.ControllerDescriptor.GetCustomAttributes(typeof(AllowAnonymousAttribute), true).Any();
        }
    }
}