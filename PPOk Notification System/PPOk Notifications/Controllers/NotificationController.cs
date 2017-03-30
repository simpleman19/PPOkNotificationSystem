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

        [HttpPost]
        public ActionResult NotificationListView()
        {
            IEnumerable<PPOk_Notifications.Models.Notification> param = new List<PPOk_Notifications.Models.Notification>();
            PPOk_Notifications.Service.SQLService serv = new PPOk_Notifications.Service.SQLService();
            ((List<PPOk_Notifications.Models.Notification>)param).AddRange(serv.GetNotifications());
            // return view

            if (Request.IsAjaxRequest())
            {
                return PartialView("NotificationListView", new System.Tuple<IEnumerable<PPOk_Notifications.Models.Notification>,
                    PPOk_Notifications.Service.SQLService>
                    (param, serv));
            }
            else
            {
                return View(new System.Tuple<IEnumerable<PPOk_Notifications.Models.Notification>,
                    PPOk_Notifications.Service.SQLService>
                    (param, serv));
            }
        }
        [HttpGet]
        public ActionResult NotificationListView(string searchString)
        {
            PPOk_Notifications.Service.SQLService serv = new PPOk_Notifications.Service.SQLService();
            List<PPOk_Notifications.Models.Notification> param = new List<PPOk_Notifications.Models.Notification>();
            List<PPOk_Notifications.Models.Notification> filtered = new List<PPOk_Notifications.Models.Notification>();
            param.AddRange(serv.GetNotifications());
            if (!String.IsNullOrEmpty(searchString))
            {
                foreach (var item in param)
                {
                    if (item.NotificationId.ToString().Contains(searchString) ||
                        item.Type.ToString().Contains(searchString) ||
                        item.PatientId.ToString().Contains(searchString) ||
                        serv.GetPatientById((int)item.PatientId).FirstName.Contains(searchString) ||
                        serv.GetPatientById((int)item.PatientId).LastName.Contains(searchString))
                    {
                        filtered.Add(item);
                    }
                }
            }
            else { filtered = param; }

            if (Request.IsAjaxRequest())
            {
                return PartialView("NotificationListView", filtered);
            }
            else
            {
                return View(filtered);
            }
        }

        public ActionResult CancelNotification(long id)
        {
            var db = new SQLService();
            db.Notification_Disable(id);
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


        /*
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
        */
    }
}