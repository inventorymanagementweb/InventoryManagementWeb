using System.Web.Mvc;

namespace InventoryManagement.WebApplication.Controllers
{
    public class GalleryController : Controller
    {

        public ActionResult BasicGallery()
        {
            return View();
        }

        public ActionResult BootstrapCarusela()
        {
            return View();
        }

        public ActionResult  SlickCarusela()
        {
            return View();
        }
	}
}