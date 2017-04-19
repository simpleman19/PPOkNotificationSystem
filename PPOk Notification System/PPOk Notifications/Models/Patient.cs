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

        [Column(Name = "send_birthday_message")]
        public bool SendBirthdayMessage { get; set; }

        [Column(Name = "send_refill_message")]
        public bool SendRefillMessage { get; set; }

        [Column(Name = "object_active")]
        public bool object_active { get; set; }

        [Column(Name = "preference_contact")]
        public PrimaryContactMethod ContactMethod { get; set; }

        public static Dictionary<long, Patient> _PatientDict;

        public static Dictionary<long, Patient> PatientDict
        {
            get
            {
                if (_PatientDict == null || PatientDictInvalid)
                {
                    System.Diagnostics.Debug.WriteLine("Reloading Patient Cache");
                    _PatientDict = new Dictionary<long, Patient>();
                    List<Patient> patients = DatabasePatientService.GetAll();
                    foreach (Patient p in patients)
                    {
                        p.LoadUserData();
                        _PatientDict.Add(p.PatientId, p);
                        PatientDictInvalid = false;
                    }
                }
                return _PatientDict;
            }
        }
        public static bool _PatientDictInvalid;

        public static bool PatientDictInvalid
        {
            get
            {
                return _PatientDictInvalid;
            }
            set
            {
                _PatientDictInvalid = value;
            }
        }

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