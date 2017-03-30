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
        public DateTime DateOfBirth { get; set; }
        public DateTime PreferedContactTime { get; set; }
        public bool SendBirthdayMessage { get; set; }
        public bool SendRefillMessage { get; set; }
        public OTP Otp { get; set; }
        [DisplayName("Contact Method")]
        [Column(Name = "contact_method")]
        public PrimaryContactMethod ContactMethod { get; set; }

        public enum PrimaryContactMethod
        {
            Text,
            Call,
            Email
        }
    }
}