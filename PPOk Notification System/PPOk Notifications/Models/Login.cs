using System;
using System.ComponentModel;
using System.Security.Cryptography;
using System.Text;
using PPOk_Notifications.Service;

namespace PPOk_Notifications.Models {
	public class Login
	{
	    public static readonly string UserIdSession = "user_id";

        [DisplayName("Login ID")]
        [Column(Name = "login_id")]
        public long LoginId { get; set; }

        [DisplayName("USer ID")]
        [Column(Name = "user_id")]
        public long UserId { get; set; }

        [DisplayName("Login Hash")]
        [Column(Name = "login_hash")]
        public byte[] LoginHash { get; set; }

        [DisplayName("Login Salt")]
        [Column(Name = "login_salt")]
        public byte[] LoginSalt { get; set; }

        [DisplayName("Login Token")]
        [Column(Name = "login_token")]
        public string LoginToken { get; set; }

	    public static Login GetLogin(string email)
	    {
            var user = DatabaseUserService.GetByEmail(email);
            return user == null ? null : DatabaseLoginService.GetByUserId(user.UserId);
	    }

	    public void SetPassword(string password)
	    {
	        LoginHash = HashPassword(password);
            DatabaseLoginService.Update(this);
        }

	    public bool CheckPassword(string password)
	    {
	        var hash = HashPassword(password);
	        return ArraysEqual(LoginHash, hash);
	    }

	    private byte[] HashPassword(string password)
	    {
	        if (LoginSalt == null)
	        {
	            LoginSalt = GenerateSalt();
	        }

	        var saltedPassword = Encoding.UTF8.GetBytes(Convert.ToBase64String(LoginSalt) + password);

	        using (var sha = new SHA512Managed())
	        {
	            return sha.ComputeHash(saltedPassword);
	        }
	    }

	    private static byte[] GenerateSalt()
	    {
	        var salt = new byte[32];
	        using (var random = new RNGCryptoServiceProvider())
	        {
	            random.GetNonZeroBytes(salt);
	        }
	        return salt;
	    }

	    private static bool ArraysEqual(byte[] first, byte[] second)
	    {
	        if (first == null || second == null)
	        {
	            return false;
	        }

	        if (first.Length != second.Length)
	        {
	            return false;
	        }

	        for (int i = 0; i < first.Length; i++)
	        {
	            if (first[i] != second[i])
	            {
	                return false;
	            }
	        }

	        return true;
	    }

        public static string GetUniqueKey(int maxSize)
        {
            char[] chars = new char[62];
            chars =
            "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890".ToCharArray();
            byte[] data = new byte[1];
            using (RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider())
            {
                crypto.GetNonZeroBytes(data);
                data = new byte[maxSize];
                crypto.GetNonZeroBytes(data);
            }
            StringBuilder result = new StringBuilder(maxSize);
            foreach (byte b in data)
            {
                result.Append(chars[b % (chars.Length)]);
            }
            return result.ToString();
        }
    }
}