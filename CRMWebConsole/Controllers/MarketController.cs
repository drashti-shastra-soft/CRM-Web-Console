using CRMWebConsole.Infrastructure.IService;
using CRMWebConsole.Infrastructure;
using CRMWebConsole.Filter;
using CRMWebConsole.Helper;
using System.Dynamic;
using System.Web.Mvc;
using System;

namespace CRMWebConsole.Controllers
{
    [Authentication]
    public class MarketController : Controller
    {
        private readonly IMarketService _service;
        private readonly IDropdownService _dropdownService;

        public MarketController(IMarketService service, IDropdownService dropdownService)
        {
            _service = service;
            _dropdownService = dropdownService;
        }

        public ActionResult Index()
        {
            return View(new Domain.MarketLister());
        }

        public ActionResult List(Domain.MarketLister mLister)
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
            ViewBag.States = _dropdownService.GetStateOptionsAsync().GetAwaiter().GetResult();

            return PartialView("_Create", new Domain.Market());
        }

        [HttpPost]
        public ActionResult Create(Domain.Market mMarket)
        {
            try
            {
                System.Threading.Thread.Sleep(1000);
                mMarket = StaticMethods.SetAuditOnUpsert(mMarket);
                mMarket = _service.CreateAsync(mMarket).GetAwaiter().GetResult();

                if (mMarket != null && mMarket.Id > 0)
                {
                    ViewBag.NotificationType = "success";
                    ViewBag.NotificationMsg = "Market has been saved successfully!";
                }
                else
                {
                    ViewBag.NotificationType = "error";
                    ViewBag.NotificationMsg = "Error occurred while saving market!";
                }
            }

            catch (Exception)
            {
                ViewBag.NotificationType = "error";
                ViewBag.NotificationMsg = "Internal server error!";
            }
            finally
            {
                ViewBag.States = _dropdownService.GetStateOptionsAsync().GetAwaiter().GetResult();
                ViewBag.Cities = _dropdownService.GetCityOptionsAsync(mMarket.State, mMarket.City).GetAwaiter().GetResult();
            }

            return PartialView("_Create", mMarket);
        }

        [HttpGet]
        public ActionResult Get(int id)
        {
            var mMarket = new Domain.Market();
            try
            {
                System.Threading.Thread.Sleep(1000);
                mMarket = _service.GetAsync(id).GetAwaiter().GetResult();
            }
            catch (Exception)
            {
                ViewBag.NotificationType = "error";
                ViewBag.NotificationMsg = "Internal server error!";
            }
            finally
            {
                ViewBag.States = _dropdownService.GetStateOptionsAsync().GetAwaiter().GetResult();
                ViewBag.Cities = _dropdownService.GetCityOptionsAsync(mMarket.State, mMarket.City).GetAwaiter().GetResult();
            }

            return PartialView("_Create", mMarket);
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            dynamic resultData = new ExpandoObject();

            try
            {
                System.Threading.Thread.Sleep(1000);    
                _service.DeleteAsync(id).GetAwaiter().GetResult();
                resultData = new { NotificationType = "success", NotificationMsg = "Market has been deleted!" };
            }
            catch (Exception)
            {
                resultData = new { NotificationType = "error", NotificationMsg = "Error occurred while deleting market!" };
            }
            return Json(resultData, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetCityBy(string state)
        {
            dynamic resultData = new ExpandoObject();
            try
            {
                System.Threading.Thread.Sleep(1000);
                var mCities = _dropdownService.GetCityOptionsAsync(state).GetAwaiter().GetResult();
                resultData = new { NotificationType = "success", Cities = mCities };
            }
            catch (Exception)
            {
                resultData = new { NotificationType = "error", NotificationMsg = "Error occurred while deleting market!" };
            }
            return Json(resultData, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult CheckRankExist(Domain.Market mMarket)
        {
            dynamic resultData = new ExpandoObject();
            try
            {
                System.Threading.Thread.Sleep(1000);
                var isExist = _service.CheckRankExistAsync(mMarket).GetAwaiter().GetResult();
                resultData = new { StatusCode = "Ok", IsExist = isExist };
            }
            catch (BadRequestException ex)
            {
                ViewBag.NotificationType = "error";
                ViewBag.NotificationMsg = ex.Message;
            }
            catch (Exception)
            {
                ViewBag.NotificationType = "error";
                ViewBag.NotificationMsg = "Internal server error!";
            }
            return Json(resultData, JsonRequestBehavior.AllowGet);
        }

    }

}