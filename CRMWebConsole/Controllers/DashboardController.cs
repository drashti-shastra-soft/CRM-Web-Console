using CRMWebConsole.Filter;
using System.Web.Mvc;

namespace CRMWebConsole.Controllers
{
    [Authentication]
    public class DashboardController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}