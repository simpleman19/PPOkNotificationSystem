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

            return View();
        }

        [HttpPost]
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
            return RedirectToAction("Index");
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
            var user = DatabaseUserService.GetByEmail(email);
            if (user == null)
            {
                return RedirectToAction("Index");
            }

            EmailService.SendReset(user);
            return RedirectToAction("ResetRequestSent");
        }

        public ActionResult ResetRequestSent()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Reset(string email, string password, string confirm_password, string otpCode)
        {
            var otp = DatabaseOtpService.GetByCode(otpCode);
            if (otp == null || !otp.IsActive())
            {
                return RedirectToAction("Index");
            }
            DatabaseOtpService.Disable(otp.Id);

            var user = Login.GetLogin(email);

            if (user == null)
            {
                return RedirectToAction("Index");
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
