﻿using System.Web.Mvc;
using PPOk_Notifications.Models;
using PPOk_Notifications.Service;

namespace PPOk_Notifications.Controllers
{
    [Authorize]
    public class LoginController : BaseController
    {
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Index(string email, string password)
        {
            // TODO get user from database
            var login = GetLogin(email);
            if (login == null)
            {
                return View(false);
            }

            if (!login.CheckPassword(password))
            {
                return View(false);
            }
            Session["user_id"] = login.UserId;
            var db = new SQLService();
            var user = db.GetUserById(login.UserId);
            if (user.Type == Models.User.UserType.PPOkAdmin)
            {
                return Redirect("/PpokAdmin/PharmacyListView");
            } else if (user.Type == Models.User.UserType.Pharmacist)
            {
                return Redirect("/Pharmacy/RefillListView");
            }
            return View(false);
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
            var user = GetLogin(email);

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

        private Login GetLogin(string email)
        {
            var user = new SQLService().GetUserByEmail(email);
            if (user == null)
            {
                return null;
            }

            return new SQLService().GetLoginByUserId(user.UserId);
        }
    }
}
