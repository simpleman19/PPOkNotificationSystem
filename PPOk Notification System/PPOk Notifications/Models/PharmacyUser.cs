using System;
using System.Security.Cryptography;

namespace PPOk_Notifications.Models
{
    public class PharmacyUser : User
    {
        public long PharmacyId { get; set; }
        public bool IsAdmin { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] Salt { get; set; }

        public void GenerateSalt()
        {
            var salt = new byte[32];
            using (var random = new RNGCryptoServiceProvider())
            {
                random.GetNonZeroBytes(salt);
            }
            Salt = salt;
        }
    }
}