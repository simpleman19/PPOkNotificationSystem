using System.Collections.Generic;
using System.Web.Mvc;
using PPOk_Notifications.Models;
using PPOk_Notifications.Service;

namespace PPOk_Notifications.Controllers
{
    public class NotificationController : Controller
    {
        // GET: Notification
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult NotificationList()
        {
            List<Notification> list = new List<Notification>(); 
            for (int i = 0; i < 100; i++)
            {
                list.Add(Notification.GetTestNotification());
            }
            return View(list);
        }

        public ActionResult DeleteNotification(long id)
        {
            var db = new SQLService();
            db.Notification_Disable(id);
            return Redirect("/Notification/NotificationList");
        }
    }
}