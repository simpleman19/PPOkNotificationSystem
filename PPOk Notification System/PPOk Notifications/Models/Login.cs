using System;
using System.Security.Cryptography;
using System.Text;

namespace PPOk_Notifications.Models {
	public class Login {
		public long UserId { get; set; }
        public byte[] LoginHash { get; set; }
        public byte[] LoginSalt { get; set; }
        public string LoginToken { get; set; }

	    public Login GetLogin(string email)
	    {
	        // TODO get user from database
            return new Login();
	    }

	    public void SetPassword(string password)
	    {
	        LoginHash = HashPassword(password);
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
	}
}