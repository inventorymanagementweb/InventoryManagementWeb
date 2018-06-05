using System.Web.Mvc;

namespace InventoryManagement.WebApplication.Controllers
{
    public class LayoutsController : Controller
    {

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult OffCanvas()
        {
            return View();
        }
	}
}