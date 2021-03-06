﻿using System.Collections.Generic;
using System.Web.Mvc;
using PPOk_Notifications.Models;
using PPOk_Notifications.Service;
using System;
using PPOk_Notifications.Filters;

namespace PPOk_Notifications.Controllers
{
    [Authenticate(Group.Pharmacist, Group.PharmacyAdmin)]
    public class NotificationController : Controller
    {
        // GET: Notification
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SendNotification(long id)
        {
            var n = DatabaseNotificationService.GetById(id);
            NotificationSending.NotificationSender.SendNotification(n);
            return Redirect("/Notification/NotificationList");
        }

        // to send a recall?
        // done by 'upload recalls,' leaving out alternative method for now
        // public ActionResult AddNotification()

        public ActionResult NotificationList()
        {
            List<Notification> notifications = DatabaseNotificationService.GetDateRange((long)Session["pharm_id"], DateTime.Now, @DateTime.Now.AddDays(7));
            ViewBag.date1 = @DateTime.Now.ToShortDateString();
            ViewBag.date2 = @DateTime.Now.AddDays(7).ToShortDateString();
            return View(notifications);
        }

        [HttpPost]
        public ActionResult GetNotifications(string datePicker1, string datePicker2)
        {
            var date1 = DateTime.Parse(datePicker1);
            var date2 = DateTime.Parse(datePicker2);
            ViewBag.date1 = date1.ToShortDateString();
            ViewBag.date2 = date2.ToShortDateString();
            List<Notification> notifications = DatabaseNotificationService.GetDateRange((long)Session["pharm_id"], date1, date2);
            return View("NotificationList", notifications);
        }

        [HttpGet]
        public ActionResult GetNotifications()
        {
            List<Notification> notifications = DatabaseNotificationService.GetDateRange((long)Session["pharm_id"], DateTime.Now, @DateTime.Now.AddDays(7));
            ViewBag.date1 = @DateTime.Now.ToShortDateString();
            ViewBag.date2 = @DateTime.Now.AddDays(7).ToShortDateString();
            return View(notifications);
        }


        public ActionResult DeleteNotification(long id)
        {
            DatabaseNotificationService.Disable(id);
            return Redirect("/Notification/NotificationList");
        }
    }
}