using PPOk_Notifications.Service;
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
            if (Request.IsAjaxRequest())
            {
                return PartialView("PharmacyListView");
            }
            else
            {
                return View();
            }
        }

        // returned view for adding, editing, or viewing a pharmacy
        public ActionResult PharmacyModificationView(int Id)
        {
            SQLService db = new SQLService();

            Models.Pharmacy pharmacy = new Models.Pharmacy(); // TODO (needs additional sql services)  = db.GetPharmacyById(Id);
            Models.PharmacyUser admin = new Models.PharmacyUser(); // TODO (needs additional sql services), search db for admin user with matching pharmacy id
            if (pharmacy.PharmacyName == "") {

                // TODO would 'if (id == null)' work better?
                // TODO can create pharmacy id here or in constructor or with sql service depending on implementation
                // TODO can create admin id here or in constructor or with sql service depending on implementation

                pharmacy = new Models.Pharmacy();
                admin = new Models.PharmacyUser();
                admin.IsAdmin = true;
                admin.PharmacyId = pharmacy.PharmacyId;
            }

            if (Request.IsAjaxRequest())
            {
                return PartialView("PharmacyModificationView", pharmacy);
            }
            else
            {
                return View(pharmacy);
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