using System;
using System.Security.Cryptography;

namespace PPOk_Notifications.Models
{
    public class PharmacyUser : User
    {
        public long PharmacyId { get; set; }
        public bool IsAdmin { get; set; }
        public byte[] PasswordHash { get; set; }
        public string Salt { get; set; }

        public void GenerateSalt()
        {
            var salt = new byte[64];
            using (var random = new RNGCryptoServiceProvider())
            {
                random.GetNonZeroBytes(salt);
            }
            Salt = Convert.ToBase64String(salt);
        }
    }
}