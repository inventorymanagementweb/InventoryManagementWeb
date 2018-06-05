using System.Web.Mvc;

namespace InventoryManagement.WebApplication.Controllers
{
    public class LandingController : Controller
    {

        public ActionResult Index()
        {
            return View();
        }
	}
}