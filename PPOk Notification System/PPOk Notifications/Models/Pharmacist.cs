using PPOk_Notifications.Service;

namespace PPOk_Notifications.Models {
    public class Pharmacist : User {

	    [Column(Name = "pharmacist_id")]
		public long PharmacistId { get; set; }

		[Column(Name = "pharmacy_id")]
		public long PharmacyId { get; set; }

		[Column(Name = "pharmacist_admin")]
		public bool IsAdmin { get; set; }

        public Pharmacist()
        {
            this.Type = UserType.Pharmacist;
        }

        public Pharmacist LoadUserData()
        {
            var user = DatabaseUserService.GetById(UserId);
            FirstName = user.FirstName;
            LastName = user.LastName;
            Email = user.Email;
            Phone = user.Phone;
            return this;
        }
    }
}