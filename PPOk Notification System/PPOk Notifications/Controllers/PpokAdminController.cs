using PPOk_Notifications.Service;
using System.Collections.Generic;
using System.Web.Mvc;

namespace PPOk_Notifications.Controllers
{
    [Authorize]
    public class PpokAdminController : BaseController
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
        /* 99.9% sure this will not be used in any capacity given new search
        [HttpGet]
        public ActionResult PharmacyListView(string searchString)
        {
            PPOk_Notifications.Service.SQLService serv = new PPOk_Notifications.Service.SQLService();
            List<PPOk_Notifications.Models.Pharmacy> param = new List<PPOk_Notifications.Models.Pharmacy>();
            //param.AddRange(serv.GetPharmacies());
            List<PPOk_Notifications.Models.Pharmacy> filtered = new List<PPOk_Notifications.Models.Pharmacy>();
            if (!String.IsNullOrEmpty(searchString))
            {
                foreach (var item in param)
                {
                    if (item.PharmacyAddress.ToString().Contains(searchString) ||
                        item.PharmacyId.ToString().Contains(searchString) ||
                        item.PharmacyName.ToString().Contains(searchString) ||
                        item.PharmacyPhone.ToString().Contains(searchString))
                    {
                        filtered.Add(item);
                    }
                }
            }
            else { filtered = param; }

            if (Request.IsAjaxRequest())
            {
                return PartialView("PharmacyListView", filtered);
            }
            else
            {
                return View(filtered);
            }
        }
        */

        // returned view for adding, editing, or viewing a pharmacy
        public ActionResult PharmacyModificationView(int id)
        {
            SQLService database = new SQLService();

            Models.Pharmacy pharmacy = new Models.Pharmacy();
            //if (id != 0)
            //    pharmacy = database.GetPharmacyById(id);

            // FIXME: phamacyuser vs pharmacist
            List<Models.Pharmacist> pharmacists = new List<Models.Pharmacist>();//database.GetPharmacists();
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

        [HttpPost]
        public void PharmacyModificationView(System.Tuple<Models.Pharmacy, Models.Pharmacist> pharmacyAndAdmin)
        {
            SQLService database = new SQLService();
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
            //database.Pharmacy_Disable((int)id);
        }
    }
}