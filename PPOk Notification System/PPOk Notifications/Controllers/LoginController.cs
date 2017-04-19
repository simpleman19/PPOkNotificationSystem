using System.Web.Mvc;
using PPOk_Notifications.Filters;
using PPOk_Notifications.Models;
using PPOk_Notifications.Service;

namespace PPOk_Notifications.Controllers
{
    public class LoginController : Controller
    {
        public ActionResult Index()
        {
            if (Session[Login.UserIdSession] != null)
            {
                var id = (long)Session[Login.UserIdSession];
                var action = RedirectToProperPage(id);
                if (action != null)
                {
                    return action;
                }
            }

            return View("Index");
        }

        [HttpPost]
        public ActionResult Index(string email, string password)
        {
            var login = Login.GetLogin(email);
            if (login == null)
            {
                return View("Index", false);
            }

            if (!login.CheckPassword(password))
            {
                return View("Index", false);
            }

            Session[Login.UserIdSession] = login.UserId;
            return RedirectToProperPage(login.UserId) ?? View("Index", false);
        }

        private ActionResult RedirectToProperPage(long userId)
        {
            var user = DatabaseUserService.GetById(userId);
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

        [Authenticate(Group.Pharmacist, Group.PharmacyAdmin, Group.PPOkAdmin)]
        public ActionResult Logout()
        {
            Session.Clear();
            return Index();
        }

        public ActionResult ResetResult(ResetResults? results)
        {
            return View("ResetResult", results);
        }

        public ActionResult ResetRequest()
        {
            return View("ResetRequest");
        }

        [HttpPost]
        public ActionResult ResetRequest(string email)
        {
            var user = DatabaseUserService.GetByEmail(email);
            if (user == null)
            {
                return Index();
            }

            EmailService.SendReset(user);
            return ResetRequestSent();
        }

        public ActionResult ResetRequestSent()
        {
            return View("ResetRequestSent");
        }

        [HttpPost]
        public ActionResult Reset(string email, string password, string confirm_password, string otpCode)
        {
            var otp = DatabaseOtpService.GetByCode(otpCode);
            if (otp == null || !otp.IsActive())
            {
                return Index();
            }
            DatabaseOtpService.Disable(otp.Id);

            var user = Login.GetLogin(email);

            if (user == null)
            {
                return Index();
            }

            if (string.IsNullOrEmpty(password) || string.IsNullOrEmpty(confirm_password))
            {
                return ResetResult(ResetResults.PasswordNotSet);
            }

            if (password != confirm_password)
            {
                return ResetResult(ResetResults.PasswordsDontMatch);
            }

            user.SetPassword(password);

            return ResetResult(null);
        }

        public enum ResetResults
        {
            PasswordNotSet,
            PasswordsDontMatch
        }

        public class ResetData
        {
            public string Email { get; set; }
            public string OTP { get; set; }
        }
    }
}
