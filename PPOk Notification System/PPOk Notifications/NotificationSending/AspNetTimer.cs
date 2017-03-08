using System;
using System.Threading;
using PPOk_Notifications.NotificationSending;


public static class AspNetTimer
{
    private static readonly Timer _timer = new Timer(OnTimerElapsed);
    private static readonly NotificationSender _notificationSender = new NotificationSender();

    public static void Start()
    {
        _timer.Change(TimeSpan.Zero, TimeSpan.FromMilliseconds(30000));
    }

    private static void OnTimerElapsed(object sender)
    {
        _notificationSender.DoWork();
    }
}