using System;
using System.Threading;

namespace PPOk_Notifications.NotificationSending
{
    public static class AspNetTimer
    {
        private static readonly Timer Timer = new Timer(OnTimerElapsed);
        private static readonly NotificationSender NotificationSender = new NotificationSender();

        public static void Start()
        {
            Timer.Change(TimeSpan.Zero, TimeSpan.FromMilliseconds(30000));
        }

        private static void OnTimerElapsed(object sender)
        {
            NotificationSender.DoWork();
        }
    }
}