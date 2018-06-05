using System.Web.Mvc;

namespace InventoryManagement.WebApplication.Controllers
{
    public class FormsController : Controller
    {

        public ActionResult BasicFroms()
        {
            return View();
        }

        public ActionResult Advanced()
        {
            return View();
        }
     
        public ActionResult Wizard()
        {
            return View();
        }
      
        public ActionResult FileUpload()
        {
            return View();
        }

        public ActionResult TextEditor()
        {
            return View();
        }

        public ActionResult Markdown()
        {
            return View();
        }

        public ActionResult Autocomplete()
        {
            return View();
        }

    }
}