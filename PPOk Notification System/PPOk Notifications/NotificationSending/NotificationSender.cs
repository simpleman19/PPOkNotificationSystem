using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading;
using WebBackgrounder;

[assembly: WebActivator.PreApplicationStartMethod(
  typeof(NotificationSending), "Start")]


namespace PPOk_Notifications.NotificationSending
{
    public class NotificationSender
    {


        private void sendNotifications()
        {

        }
    }
}