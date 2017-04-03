﻿using PPOk_Notifications.Service;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Web;
using System.Web.Mvc;

namespace PPOk_Notifications.Controllers
{
    [Authorize]
    public class PpokAdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            if (Request.IsAjaxRequest())
            {
                return PartialView("Index");
            }
            else
            {
                return View();
            }
        }

        // returned view for seeing a list of pharmacies
        // can add to list
        // can select edit/view/delete from list
        //[HttpPost]
        public ActionResult PharmacyListView()
        {
            PPOk_Notifications.Service.SQLService serv = new PPOk_Notifications.Service.SQLService();
            IEnumerable<PPOk_Notifications.Models.Pharmacy> param = new List<PPOk_Notifications.Models.Pharmacy>();
            //((List<PPOk_Notifications.Models.Pharmacy>)param).AddRange(serv.GetPharmacies());
            if (Request.IsAjaxRequest())
            {
                return PartialView("PharmacyListView", param);
            }
            else
            {
                return View(param);
            }
        }

        // returned view for adding, editing, or viewing a pharmacy
        public ActionResult PharmacyModificationView(int id)
        {
            SQLService database = new SQLService();

            Models.Pharmacy pharmacy = new Models.Pharmacy();
            if (id != 0)
                pharmacy = database.GetPharmacyById(id);

            List<Models.Pharmacist> pharmacists = database.GetPharmacists();
            Models.Pharmacist admin = new Models.Pharmacist();
            admin.IsAdmin = true;
            foreach (var pharmacist in pharmacists) { if (pharmacist.IsAdmin && pharmacist.PharmacyId == pharmacy.PharmacyId) { admin = pharmacist; } }

            System.Tuple<Models.Pharmacy, Models.Pharmacist> param = new System.Tuple<Models.Pharmacy, Models.Pharmacist>(pharmacy, admin);

            if (Request.IsAjaxRequest())
            {
                return PartialView("PharmacyModificationView", param);
            }
            else
            {
                return View(param);
            }
        }

        public ActionResult AddPharmacy()
        {
            return Redirect("PharmacyModificationView/0");
        }
        public ActionResult EditPharmacy(long id)
        {
            return Redirect("PharmacyModificationView/" + id.ToString());
        }
        public ActionResult ViewPharmacy(long id)
        {
            return Redirect("PharmacyModificationView/" + id.ToString());
        }
        public void DeletePharmacy(long id)
        {
            SQLService database = new SQLService();
            database.Pharmacy_Disable((int)id);
        }
    }
}