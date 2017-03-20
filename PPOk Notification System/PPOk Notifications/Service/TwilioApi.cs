using System;
using PPOk_Notifications.Models;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;
using Twilio;

namespace PPOk_Notifications.Service
{
    public class TwilioApi
    {
        private const string AccountSid = "ACXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX";
        private const string AuthToken = "an_auth_token";
        Pharmacy pharmacy;

        TwilioApi(Pharmacy pharm)
        {
            TwilioClient.Init(AccountSid, AuthToken);
            pharmacy = pharm;
        }

        public void SendTextMessage(Notification notification)
        {
            // TODO Look up patient phone number in database from notification patient id
            Patient patient = new Patient();
            Template temp = GetTempFromPharmacy(notification.Type);

            var message = MessageResource.Create(
                to: new PhoneNumber("+19999999999"),
                from: new PhoneNumber("+19999999998"),
                body: temp.TemplateText);
        }

        public void MakePhoneCall(Notification notification)
        {
            // TODO Look up patient phone number in database from notification patient id
            Patient patient = new Patient();

            var to = new PhoneNumber("+14155551212");
            var from = new PhoneNumber("+15017250604");
            var call = CallResource.Create(to,
                                           from,
                                           url: new Uri("http://demo.twilio.com/docs/voice.xml"));
            //TODO create xmls for phone calls
        }

        private Template GetTempFromPharmacy(Notification.NotificationType type)
        {
            Template temp = null;
            switch (type)
            {
                case Notification.NotificationType.Refill:
                    temp = pharmacy.GetRefillTemplate();
                    break;
                case Notification.NotificationType.Recall:
                    temp = pharmacy.GetRecallTemplate();
                    break;
                case Notification.NotificationType.Refilled:
                    temp = pharmacy.GetRefilledTemplate();
                    break;
                case Notification.NotificationType.Birthday:
                    temp = pharmacy.GetBirthdayTemplate();
                    break;
            }
            return temp;
        }

    }

}