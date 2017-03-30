using PPOk_Notifications.Service;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Web;
using System.Web.Mvc;

namespace PPOk_Notifications.Controllers
{
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
        public ActionResult PharmacyListView()
        {
            PPOk_Notifications.Service.SQLService serv = new PPOk_Notifications.Service.SQLService();
            IEnumerable<PPOk_Notifications.Models.Pharmacy> param = new List<PPOk_Notifications.Models.Pharmacy>();
            ((List<PPOk_Notifications.Models.Pharmacy>)param).AddRange(serv.GetPharmacies());
            if (Request.IsAjaxRequest())
            {
                return PartialView("PharmacyListView",param);
            }
            else
            {
                return View(param);
            }
        }

        // returned view for adding, editing, or viewing a pharmacy
        public ActionResult PharmacyModificationView(int Id)
        {
            //TODO needs sql service support SQLService db = new SQLService();

            Models.Pharmacy pharmacy = new Models.Pharmacy(); // TODO (needs additional sql services)  = db.GetPharmacyById(Id);
            Models.PharmacyUser admin = new Models.PharmacyUser(); // TODO (needs additional sql services), search db for admin user with matching pharmacy id
            if (pharmacy.PharmacyName == "") {

                // TODO would 'if (id == null)' work better?
                // TODO can create pharmacy id here or in constructor or with sql service depending on implementation
                // TODO can create admin id here or in constructor or with sql service depending on implementation
                /*
                pharmacy = new Models.Pharmacy();
                admin = new Models.PharmacyUser();
                admin.IsAdmin = true;
                admin.PharmacyId = pharmacy.PharmacyId;
                */
            }

            System.Tuple<Models.Pharmacy,Models.PharmacyUser> param = new System.Tuple<Models.Pharmacy, Models.PharmacyUser>(pharmacy, admin);

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
        public void PharmacyModificationView(System.Tuple<Models.Pharmacy, Models.PharmacyUser> pharmacyAndAdmin)
        {
            SQLService database = new SQLService();
            // TODO (needs more sql services) database.UpdatePharmacy(pharmacyAndAdmin.Item1); database.UpdatePharmacy(pharmacyAndAdmin.Item2);
        }

        // TODO AddPharmacy
        // TODO EditPharmacy long id
        // TODO ViewPharmacy long id
        // TODO DeletePharmacy   see vv
        public void DeletePharmacy(long id)
        {
            /*
             * TODO
             * could do this multiple ways
                <%= 
                    Html.ActionLink( 
                                    "Label", 
                                    "Action",  
                                    "Controller",
                                    new {Parameter1 = Model.Data1, Parameter2 = Model.Data2},
                                    null
                                   ) 
                %>  
             *
             * 
             * 

            SQLService db = new SQLService();
            if (id != 0)
            {
                // TODO delete the pharmacy
            }
            else
            {
                // TODO delete the pharmacy
            }
            */
        }
    }
}