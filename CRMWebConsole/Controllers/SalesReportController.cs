using CRMWebConsole.Filter;
using CRMWebConsole.Helper;
using CRMWebConsole.Infrastructure.IService;
using System;
using System.Web.Mvc;

namespace CRMWebConsole.Controllers
{
    [Authentication]
    public class SalesReportController : Controller
    {
        private readonly ISalesReportService _service;
        private readonly IDropdownService _dropdownService;

        public SalesReportController(ISalesReportService service, IDropdownService dropdownService)
        {
            _service = service;
            _dropdownService = dropdownService;
        }

        public ActionResult Index()
        {
            return View(new Domain.SalesReportLister());
        }

        public ActionResult List(Domain.SalesReportLister mLister)
        {
            try
            {
                System.Threading.Thread.Sleep(1000);
                mLister = _service.GetAllAsync(mLister).GetAwaiter().GetResult();
            }
            catch
            {
                ViewBag.NotificationType = "error";
                ViewBag.NotificationMsg = "Something went wrong!";
            }
            finally
            {
                mLister.Pagination.PaginationSummary = PaginationHelper.GetPaginationSummary(mLister.Pagination);
                ViewBag.Pagination = _dropdownService.GetPageSizeOptions(mLister.Pagination.PageSize);

                // Current month with year
                ViewBag.CurrentMonth = DateTime.Now.ToString("MMMM yyyy");

                // Previous month with year
                ViewBag.PreviousMonth = DateTime.Now.AddMonths(-1).ToString("MMMM yyyy");

                // Previous to previous month with year
                ViewBag.PreviousToPreviousMonth = DateTime.Now.AddMonths(-2).ToString("MMMM yyyy");
            }
            return PartialView("_List", mLister);
        }

    }
}