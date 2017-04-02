using System.Collections.Generic;
using System.Web.Mvc;
using PPOk_Notifications.Models;
using System.Collections.Generic;
using PPOk_Notifications.Service;
using System;

namespace PPOk_Notifications.Controllers
{
    public class NotificationController : Controller
    {
        // GET: Notification
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CancelNotification(long id)
        {
            var db = new SQLService();
            //db.Notification_Disable(id);
            return Redirect("/Notification/NotificationListView");
        }


        public ActionResult AddNotification()
        {
            // TODO: will need a notification input view or model
            if (Request.IsAjaxRequest())
            {
                return PartialView("NotificationListView");
            }
            else
            {
                return View();
            }
        }

        public ActionResult NotificationList()
        {
            var db = new SQLService();
            List<Notification> notifications = new List<Notification>();
            // sql error --> db.GetNotificationsActive();
            if (notifications.Count == 0)
            {
                Notification n = null;
                Random rand = new Random();
                for (int i = 0; i < 100; i++)
                {
                    n = Notification.GetTestNotification(rand);
                    // sql error ---> db.NotificationInsert(n);
                    notifications.Add(n);
                }
                // sql error ---> notifications = db.GetNotificationsActive();
            }
            return View(notifications);
        }

        public ActionResult DeleteNotification(long id)
        {
            var db = new SQLService();
            //db.Notification_Disable(id);
            return Redirect("/Notification/NotificationList");
        }
    }
}