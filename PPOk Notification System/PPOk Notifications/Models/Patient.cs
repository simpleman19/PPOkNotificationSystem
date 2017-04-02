using System;
using System.Collections.Generic;
using System.ComponentModel;
using PPOk_Notifications.Service;


namespace PPOk_Notifications.Models
{
    [Serializable, DisplayName("Patient")]
    public class Patient : User
    {
        [DisplayName("Patient ID")]
        [Column(Name = "patient_id")]
        public long PatientId { get; set; }
        [DisplayName("Person Code")]
        [Column(Name = "person_code")]
        public int PersonCode { get; set; }
        [Column(Name = "pharmacy_id")]
        public long PharmacyId { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime PreferedContactTime { get; set; }
        public bool SendBirthdayMessage { get; set; }
        public bool SendRefillMessage { get; set; }
        public OTP Otp { get; set; }
        [DisplayName("Contact Method")]
        [Column(Name = "contact_method")]
        public PrimaryContactMethod ContactMethod { get; set; }

        public Patient()
        {
            this.Type = UserType.Patient;
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
            return new Pharmacy();
        }

        public static Patient getTestPatient()
        {
            var p = new Patient();
            p.FirstName = "Tom";
            p.LastName = "Doe";
            p.UserId = 123;
            p.Phone = "+19999999999";
            return p;
        }
    }
}