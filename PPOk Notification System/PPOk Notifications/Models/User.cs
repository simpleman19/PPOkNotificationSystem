using PPOk_Notifications.Service;

namespace PPOk_Notifications.Models
{
    public class User
    {
		[Column(Name = "user_lname")] public string LastName { get; set; }

        [Column(Name = "user_fname")] public string FirstName { get; set; }

        [Column(Name = "user_id")] public long UserId { get; set; }

		[Column(Name = "user_phone")] public string Phone { get; set; }

		[Column(Name = "user_email")] public string Email { get; set; }

		[Column(Name = "user_type")] public UserType Type { get; set; }

		[Column(Name = "object_active")] public bool Enabled { get; set; }

		public Login UserLogin { get; set; }

        public enum UserType
        {
            Pharmacist,
            PPOkAdmin,
            Patient
        }

        public string GetFullName()
        {
            return LastName + ", " + FirstName;
        }

	    public User LoadUserData() {
		    var user = DatabaseUserService.GetById(UserId);
		    LastName = user.LastName;
		    FirstName = user.FirstName;
		    Phone = user.Phone;
		    Email = user.Email;
		    Type = user.Type;
		    Enabled = user.Enabled;
		    return this;
	    }
	}
}