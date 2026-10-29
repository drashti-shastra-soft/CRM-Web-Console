using CRMWebConsole.Filter;
using CRMWebConsole.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRMWebConsole.Controllers
{
    [Authentication]
    public class LogoutController : Controller
    {
        public ActionResult Index()
        {
            HttpContextHelper.LoginUser = null;
            return RedirectToAction("Index", "Authentication");
        }
    }
}