using System.Net;
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
        public ActionResult Index(string username, string password)
        {
            // TODO get user from database
            var user = new User();

            if (!(user is PharmacyUser))
            {
                return View(LoginResult.UserNotFound);
            }

            var pharmacyUser = (PharmacyUser) user;
            if (string.IsNullOrEmpty(pharmacyUser.Salt))
            {
                pharmacyUser.GenerateSalt();
            }

            if (pharmacyUser.PasswordHash == null)
            {
                return View(LoginResult.PasswordNotSet);
            }

            var saltedPassword = Encoding.UTF8.GetBytes(pharmacyUser.Salt + password);

            byte[] hash;
            using (var sha = new SHA512Managed())
            {
                hash = sha.ComputeHash(saltedPassword);
            }

            saltedPassword = null;
            password = null;

            if (!ArraysAreEqual(pharmacyUser.PasswordHash, hash))
            {
                return View(LoginResult.WrongPassword);
            }

            return RedirectToAction("Success");
        }

        public ActionResult Success()
        {
            return View();
        }

        public enum LoginResult
        {
            WrongPassword,
            UserNotFound,
            PasswordNotSet
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
    }
}