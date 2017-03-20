using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PPOk_Notifications.Models;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;
using Twilio;

namespace PPOk_Notifications.Service
{
    public class TwilioApi
    {
        String accountSid = "ACXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX";
        String authToken = "an_auth_token";
        Pharmacy pharmacy;

        TwilioApi(Pharmacy pharm)
        {
            TwilioClient.Init(accountSid, authToken);
            pharmacy = pharm;
        }

        public void sendTextMessage(Notification notification)
        {
            // TODO Look up patient phone number in database from notification patient id
            Patient patient = new Patient();
            Template temp = getTempFromPharmacy(notification.notificationType);

            var message = MessageResource.Create(
                to: new PhoneNumber("+19999999999"),
                from: new PhoneNumber("+19999999998"),
                body: temp.templateText);
        }

        public void makePhoneCall(Notification notification)
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

        private Template getTempFromPharmacy(Notification.NotificationType type)
        {
            Template temp = null;
            switch (type)
            {
                case Notification.NotificationType.Refill:
                    temp = pharmacy.getRefillTemplate();
                    break;
                case Notification.NotificationType.Recall:
                    temp = pharmacy.getRecallTemplate();
                    break;
                case Notification.NotificationType.Refilled:
                    temp = pharmacy.getRefilledTemplate();
                    break;
                case Notification.NotificationType.Birthday:
                    temp = pharmacy.getBirthdayTemplate();
                    break;
            }
            return temp;
        }

    }

}