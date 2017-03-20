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
            return View();
        }

        // returned view for seeing a list of pharmacies
        // can add to list
        // can select edit/view/delete from list
        public ActionResult PharmacyListView()
        {
            return View();
        }

        // returned view for adding, editing, or viewing a pharmacy
        public ActionResult PharmacyModificationView()
        {
            return View();
        }
    }
}