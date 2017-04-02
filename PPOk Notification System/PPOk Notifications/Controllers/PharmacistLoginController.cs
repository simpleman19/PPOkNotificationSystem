using System;
using System.Security.Cryptography;
using System.Text;
using System.Web.Mvc;
using PPOk_Notifications.Models;

namespace PPOk_Notifications.Controllers
{
    public class PharmacistLoginController : Controller
    {
        // GET: PharmacistLogin
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string email, string password)
        {
            // TODO get user from database
            var user = GetUser(email);
            if (user == null)
            {
                return View(LoginResult.UserNotFound);
            }

            if (user.PasswordHash == null)
            {
                return View(LoginResult.PasswordNotSet);
            }

            var hash = HashPassword(user, password);

            if (!ArraysAreEqual(user.PasswordHash, hash))
            {
                return View(LoginResult.WrongPassword);
            }
            Session["user_id"] = user.UserId;
            return RedirectToAction("Success");
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
            return RedirectToAction("Reset", "PharmacistLogin", new { email = email});
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
            var user = GetUser(email);

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

            user.PasswordHash = HashPassword(user, password);

            return RedirectToAction("ResetResult");
        }

        public enum LoginResult
        {
            WrongPassword,
            UserNotFound,
            PasswordNotSet
        }

        public enum ResetResults
        {
            UserNotFound,
            PasswordNotSet,
            PasswordsDontMatch
        }

        private Pharmacist GetUser(string email)
        {
            // TODO get user from database
            var user = new Pharmacist();
            user.Email = email;

            return user;
        }

        private bool ArraysAreEqual(byte[] firstArray, byte[] secondArray)
        {
            if (firstArray == null || secondArray == null)
            {
                return false;
            }

            if (firstArray.Length != secondArray.Length)
            {
                return false;
            }

            for (int i = 0; i < firstArray.Length; i++)
            {
                if (firstArray[i] != secondArray[i])
                {
                    return false;
                }
            }

            return true;
        }

        private byte[] HashPassword(Pharmacist user, string password)
        {
            if (user.Salt == null)
            {
                user.GenerateSalt();
            }

            var saltedPassword = Encoding.UTF8.GetBytes(Convert.ToBase64String(user.Salt) + password);

            byte[] hash;
            using (var sha = new SHA512Managed())
            {
                hash = sha.ComputeHash(saltedPassword);
            }

            return hash;
        }
    }
}
