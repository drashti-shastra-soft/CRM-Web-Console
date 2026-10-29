using CRMWebConsole.Filter;
using CRMWebConsole.Helper;
using CRMWebConsole.Infrastructure;
using CRMWebConsole.Infrastructure.IService;
using System;
using System.Dynamic;
using System.Web.Mvc;

namespace CRMWebConsole.Controllers
{
    [Authentication]
    public class CityController : Controller
    {
        private readonly ICityService _service;
        private readonly IDropdownService _dropdownService;

        public CityController(ICityService service, IDropdownService dropdownService)
        {
            _service = service;
            _dropdownService = dropdownService;
        }

        public ActionResult Index()
        {
            return View(new Domain.CityLister());
        }

        public ActionResult List(Domain.CityLister mLister)
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
                ViewBag.Status = _dropdownService.GetStatusOptions();
                ViewBag.Pagination = _dropdownService.GetPageSizeOptions(mLister.Pagination.PageSize);
            }
            return PartialView("_List", mLister);
        }

        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.States = _dropdownService.FetchStateOptionsAsync().GetAwaiter().GetResult();

            return PartialView("_Create", new Domain.City());
        }

        [HttpPost]
        public ActionResult Create(Domain.City mCity)
        {
            try
            {
                System.Threading.Thread.Sleep(1000);
                mCity = StaticMethods.SetAuditOnUpsert(mCity);
                mCity = _service.CreateAsync(mCity).GetAwaiter().GetResult();

                if (mCity != null && mCity.Id > 0)
                {
                    ViewBag.NotificationType = "success";
                    ViewBag.NotificationMsg = "City has been saved successfully!";
                }
                else
                {
                    ViewBag.NotificationType = "error";
                    ViewBag.NotificationMsg = "Error occurred while saving city!";
                }
            }

            catch (Exception)
            {
                ViewBag.NotificationType = "error";
                ViewBag.NotificationMsg = "Internal server error!";
            }
            finally
            {
                ViewBag.States = _dropdownService.FetchStateOptionsAsync().GetAwaiter().GetResult();
            }

            return PartialView("_Create", mCity);
        }

        [HttpGet]
        public ActionResult Get(int id)
        {
            var mCity = new Domain.City();
            try
            {
                System.Threading.Thread.Sleep(1000);
                mCity = _service.GetAsync(id).GetAwaiter().GetResult();
            }
            catch (Exception)
            {
                ViewBag.NotificationType = "error";
                ViewBag.NotificationMsg = "Internal server error!";
            }
            finally
            {
                ViewBag.States = _dropdownService.FetchStateOptionsAsync().GetAwaiter().GetResult();
            }

            return PartialView("_Create", mCity);
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            dynamic resultData = new ExpandoObject();

            try
            {
                System.Threading.Thread.Sleep(1000);
                _service.DeleteAsync(id).GetAwaiter().GetResult();
                resultData = new { NotificationType = "success", NotificationMsg = "City has been deleted!" };
            }
            catch (Exception)
            {
                resultData = new { NotificationType = "error", NotificationMsg = "Error occurred while deleting city!" };
            }
            return Json(resultData, JsonRequestBehavior.AllowGet);
        }
    }
}