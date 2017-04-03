using System.Web.Mvc;
using PPOk_Notifications.Filters;
using PPOk_Notifications.Models;
using PPOk_Notifications.Service;

namespace PPOk_Notifications.Controllers
{
    [Authenticate]
    public class LoginController : Controller
    {
        [AllowAnonymous]
        public ActionResult Index()
        {
            if (Session[Login.UserIdSession] != null)
            {
                long id = (long)Session[Login.UserIdSession];
                var action = RedirectToProperPage(id);
                if (action != null)
                {
                    return action;
                }
            }

            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Index(string email, string password)
        {
            var login = Login.GetLogin(email);
            if (login == null)
            {
                return View(false);
            }

            if (!login.CheckPassword(password))
            {
                return View(false);
            }

            Session[Login.UserIdSession] = login.UserId;
            return RedirectToProperPage(login.UserId) ?? View(false);
        }

        private ActionResult RedirectToProperPage(long userId)
        {
            var db = new SQLService();
            var user = db.GetUserById(userId);
            if (user.Type == Models.User.UserType.PPOkAdmin)
            {
                return Redirect("/PpokAdmin/PharmacyListView");
            }
            if (user.Type == Models.User.UserType.Pharmacist)
            {
                return Redirect("/Pharmacy/RefillListView");
            }
            return null;
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Index");
        }

        public ActionResult Success()
        {
            return View();
        }

        public ActionResult ResetResult()
        {
            return View();
        }

        public ActionResult ResetRequest()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ResetRequest(string email)
        {
            // TODO: Send email with reset token instead
            return RedirectToAction("Reset", "Login", new { email = email});
        }

        public ActionResult Reset(string email)
        {
            // TODO add reset token (which is sent to them in the email link)
            if (email == null)
            {
                return RedirectToAction("Index");
            }

            return View((object)email);
        }

        [HttpPost]
        public ActionResult Reset(string email, string password, string confirm_password)
        {
            // TODO add reset token (which is sent to them in the email link)
            var user = Login.GetLogin(email);

            if (user == null)
            {
                return RedirectToAction("ResetResult", ResetResults.UserNotFound);
            }

            if (string.IsNullOrEmpty(password) || string.IsNullOrEmpty(confirm_password))
            {
                return RedirectToAction("ResetResult", ResetResults.PasswordNotSet);
            }

            if (password != confirm_password)
            {
                return RedirectToAction("ResetResult", ResetResults.PasswordsDontMatch);
            }

            user.SetPassword(password);

            return RedirectToAction("ResetResult");
        }

        public enum ResetResults
        {
            UserNotFound,
            PasswordNotSet,
            PasswordsDontMatch
        }
    }
}
