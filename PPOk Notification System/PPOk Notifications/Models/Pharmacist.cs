﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Security.Cryptography;

namespace PPOk_Notifications.Models {
    public class Pharmacist : User
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

        public static byte[] HashPassword(Pharmacist user, string password)
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