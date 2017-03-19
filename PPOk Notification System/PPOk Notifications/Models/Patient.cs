using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PPOk_Notifications.Models
{
    public class Patient : User
    {
        public long patientID { get; set; }
        public DateTime dateOfBirth { get; set; }
        public DateTime preferedContactTime;
        public Boolean sendBirthdayMessage;
        public Boolean sendRefillMessage;
        public OneTimePass oneTimePass { get; set; }

        public enum PrimaryContactMethod
        {
            Text,
            Call,
            Email
        };

        public PrimaryContactMethod primaryContactMethod;

        public Boolean setPrimaryContact(PrimaryContactMethod primaryContMethod)
        {
            if ((int)primaryContMethod == 0)
            {
                primaryContactMethod = PrimaryContactMethod.Text;
                return true;
            }
            else if ((int)primaryContMethod == 1)
            {
                primaryContactMethod = PrimaryContactMethod.Call;
                return true;
            }
            else if ((int)primaryContMethod == 2)
            {
                primaryContactMethod = PrimaryContactMethod.Email;
                return true;
            }
            else
                return false;
        }

        public Boolean setPreferredContactTime(DateTime time)
        {
            preferedContactTime = time;
            return true;
        }

        public Boolean setBirthdayMessage(Boolean yesOrno)
        {
            sendBirthdayMessage = yesOrno;

            if (sendBirthdayMessage == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public Boolean setRefillMessage(Boolean yesOrno)
        {
            sendRefillMessage = yesOrno;

            if (sendRefillMessage == true)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
    }
}