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
            /* TODO
             * will need to load info for an existing pharmacy if edit or view was selected
             * but won't need to if 'add' was selected
             * 
            SQLService db = new SQLService();
            BadgeType id = db.GetPharmacyById(Id);

            if (Request.IsAjaxRequest())
            {
                return PartialView("PharmacyModificationView", id);
            }
            else
            {
                return View(id);
            }
            */

            return View();
        }

        // TODO add pharmacy
        // TODO edit pharmacy
        // TODO view pharmacy
        // TODO submit pharmacy modification
        public void DeletePharmacy(int id)
        {
            /*
             * TODO
             * could do this multiple ways
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