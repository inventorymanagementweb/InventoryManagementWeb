using System.Web.Mvc;

namespace InventoryManagement.WebApplication.Controllers
{
    public class TablesController : Controller
    {

        public ActionResult StaticTables()
        {
            return View();
        }

        public ActionResult DataTables()
        {
            return View();
        }

        public ActionResult FooTables()
        {
            return View();
        }

        public ActionResult JqGrid()
        {
            return View();
        }
	}
}