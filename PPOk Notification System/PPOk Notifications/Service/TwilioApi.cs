using System;
using PPOk_Notifications.Models;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;
using Twilio;
using System.Collections.Generic;

namespace PPOk_Notifications.Service
{
    public class TwilioApi
    {
        private const string AccountSid = "AC2fcaf6f7256a0f7891903318195c9e01";
        private const string AuthToken = "7504188a03f5969ff3549eaf8fba3e9c";

        Pharmacy pharmacy;

        bool testTwilio = true;

        public TwilioApi(Pharmacy pharm)
        {
            TwilioClient.Init(AccountSid, AuthToken);
            pharmacy = pharm;
        }

        public void SendTextMessage(Notification notification)
        {
            var p = DatabasePatientService.GetById(notification.PatientId);
            p.LoadUserData();
            var temp = GetTempFromPharmacy(notification.Type);
            if (testTwilio)
            {
                var message = MessageResource.Create(
                    to: new PhoneNumber(p.Phone),
                    from: new PhoneNumber("+14052469892 "),
                    body: temp.TemplateText);
            }

        }

        public void SendTextMessage(Patient p, String messageString)
        {
            p.LoadUserData();
            if (testTwilio)
            {
                var message = MessageResource.Create(
                    to: new PhoneNumber(p.Phone),
                    from: new PhoneNumber("+14052469892 "),
                    body: messageString);
            }
        }

        public void MakePhoneCall(Notification notification)
        {
            var p = DatabasePatientService.GetById(notification.PatientId);
            p.LoadUserData();
            if (testTwilio)
            {
                var to = new PhoneNumber(p.Phone);
                var from = new PhoneNumber("+14052469892");
                Uri callback_url = null;
                switch (notification.Type)
                {
                    case Notification.NotificationType.Refill:
                        callback_url = new Uri("http://ocharambe.localtunnel.me/twilioresponse/refill/" + pharmacy.PharmacyId);
                        break;
                    case Notification.NotificationType.Ready:
                        callback_url = new Uri("http://ocharambe.localtunnel.me/twilioresponse/ready/" + pharmacy.PharmacyId);
                        break;
                    case Notification.NotificationType.Birthday:
                        callback_url = new Uri("http://ocharambe.localtunnel.me/twilioresponse/birthday" + pharmacy.PharmacyId);
                        break;
                }
                var call = CallResource.Create(to,
                                               from,
                                               url: callback_url);

            }
        }

        public void MakeRecallPhoneCall(Notification notification)
        {
            Patient p = DatabasePatientService.GetById(notification.PatientId);
            p.LoadUserData();
            if (testTwilio)
            {
                var to = new PhoneNumber(p.Phone);
                var from = new PhoneNumber("+14052469892");
                var call = CallResource.Create(to,
                                               from,
                                               url: new Uri("http://demo.twilio.com/docs/voice.xml"));

            }
            //TODO create xmls for phone calls
            this.SendTextMessage(notification);
        }


        private Template GetTempFromPharmacy(Notification.NotificationType type)
        {
            Template temp = null;
            pharmacy.GetTemplates();
            switch (type)
            {
                case Notification.NotificationType.Refill:
                    temp = pharmacy.GetRefillTemplate();
                    break;
                case Notification.NotificationType.Recall:
                    temp = pharmacy.GetRecallTemplate();
                    break;
                case Notification.NotificationType.Ready:
                    temp = pharmacy.GetReadyTemplate();
                    break;
                case Notification.NotificationType.Birthday:
                    temp = pharmacy.GetBirthdayTemplate();
                    break;
            }
            return temp;
        }

    }

}