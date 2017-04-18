using System;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PPOk_Notifications.Models;
using PPOk_Notifications.Service;

namespace PPOk_Notifications.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
    public class AuthenticateAttribute : ActionFilterAttribute
    {
        private readonly Group[] _groups;

        public AuthenticateAttribute(params Group[] groups)
        {
            _groups = groups;
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (SkipAuthorization(filterContext))
            {
                return;
            }

            var userId = HttpContext.Current.Session[Login.UserIdSession];

            if (userId == null)
            {
                filterContext.Result = new RedirectResult("/Login/Index");
                return;
            }

            var user = DatabaseUserService.GetById((long)userId);
            if (user == null)
            {
                filterContext.Result = new RedirectResult("/Login/Index");
                return;
            }

            switch (user.Type)
            {
                case User.UserType.Pharmacist:
                    var pharmacist = DatabasePharmacistService.GetById((long) userId);
                    if (pharmacist.IsAdmin)
                    {
                        if (!_groups.Contains(Group.PharmacyAdmin))
                        {
                            filterContext.Result = new RedirectResult("/Login/Index");
                            return;
                        }
                    }
                    else
                    {
                        if (!_groups.Contains(Group.Pharmacist))
                        {
                            filterContext.Result = new RedirectResult("/Login/Index");
                            return;
                        }
                    }
                    break;
                case User.UserType.PPOkAdmin:
                    if (!_groups.Contains(Group.PPOkAdmin))
                    {
                        filterContext.Result = new RedirectResult("/Login/Index");
                        return;
                    }
                    break;
                case User.UserType.Patient:
                    if (!_groups.Contains(Group.Patient))
                    {
                        filterContext.Result = new RedirectResult("/Login/Index");
                        return;
                    }
                    break;
            }

            base.OnActionExecuting(filterContext);
        }

        private static bool SkipAuthorization(ActionExecutingContext filterContext)
        {
            Contract.Assert(filterContext != null);

            return filterContext.ActionDescriptor.GetCustomAttributes(typeof(AllowAnonymousAttribute), true).Any()
                   || filterContext.ActionDescriptor.ControllerDescriptor.GetCustomAttributes(typeof(AllowAnonymousAttribute), true).Any();
        }
    }
}