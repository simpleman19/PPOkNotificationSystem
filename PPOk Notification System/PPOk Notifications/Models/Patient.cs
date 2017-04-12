using System;
using System.Collections.Generic;
using System.ComponentModel;
using PPOk_Notifications.Service;


namespace PPOk_Notifications.Models
{
    [Serializable, DisplayName("Patient")]
    public class Patient : User
    {
        [Column(Name = "patient_id")]
        public long PatientId { get; set; }

        [Column(Name = "person_code")]
        public string PersonCode { get; set; }

        [Column(Name = "pharmacy_id")]
        public long PharmacyId { get; set; }

		[Column(Name = "patient_dob")]
		public DateTime DateOfBirth { get; set; }

		[Column(Name = "preference_time")]
		public DateTime PreferedContactTime { get; set; }

		public bool SendBirthdayMessage { get; set; }

		public bool SendRefillMessage { get; set; }

		[Column(Name = "object_active")]
		public bool object_active { get; set; }

        [Column(Name = "preference_contact")]
        public PrimaryContactMethod ContactMethod { get; set; }

        public Patient()
        {
            this.Type = UserType.Patient;
            this.SendBirthdayMessage = true;
            this.SendRefillMessage = true;
            this.ContactMethod = PrimaryContactMethod.Call;
        }

        public enum PrimaryContactMethod
        {
            Text,
            Call,
            Email,
            OptOut
        }

        public Pharmacy getPharmacy()
        {
            var pharm = DatabasePharmacyService.GetByIdActive(this.PharmacyId);
            pharm.GetTemplates();
            return pharm;
        }

        public static Patient getTestPatient()
        {
	        var p = new Patient {
		        FirstName = "Tom",
		        LastName = "Doe",
		        UserId = 123,
		        Phone = "+19999999999"
	        };
	        return p;
        }
    }

}