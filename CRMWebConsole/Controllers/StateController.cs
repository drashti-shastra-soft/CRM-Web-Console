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
    public class StateController : Controller
    {
        private readonly IStateService _service;
        private readonly IDropdownService _dropdownService;

        public StateController(IStateService service, IDropdownService dropdownService)
        {
            _service = service;
            _dropdownService = dropdownService;
        }

        public ActionResult Index()
        {
            return View(new Domain.StateLister());
        }

        public ActionResult List(Domain.StateLister mLister)
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
            return PartialView("_Create", new Domain.State());
        }

        [HttpPost]
        public ActionResult Create(Domain.State mState)
        {
            try
            {
                System.Threading.Thread.Sleep(1000);
                mState = StaticMethods.SetAuditOnUpsert(mState);
                mState = _service.CreateAsync(mState).GetAwaiter().GetResult();

                if (mState != null && mState.Id > 0)
                {
                    ViewBag.NotificationType = "success";
                    ViewBag.NotificationMsg = "State has been saved successfully!";
                }
                else
                {
                    ViewBag.NotificationType = "error";
                    ViewBag.NotificationMsg = "Error occurred while saving state!";
                }
            }

            catch (Exception)
            {
                ViewBag.NotificationType = "error";
                ViewBag.NotificationMsg = "Internal server error!";
            }
            finally
            {
                ViewBag.Status = _dropdownService.GetStatusOptions();
            }
            return PartialView("_Create", mState);
        }

        [HttpGet]
        public ActionResult Get(int id)
        {
            var mState = new Domain.State();
            try
            {
                System.Threading.Thread.Sleep(1000);
                mState = _service.GetAsync(id).GetAwaiter().GetResult();
            }
            catch (Exception)
            {
                ViewBag.NotificationType = "error";
                ViewBag.NotificationMsg = "Internal server error!";
            }
            return PartialView("_Create", mState);
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            dynamic resultData = new ExpandoObject();

            try
            {
                System.Threading.Thread.Sleep(1000);
                _service.DeleteAsync(id).GetAwaiter().GetResult();
                resultData = new { NotificationType = "success", NotificationMsg = "State has been deleted!" };
            }
            catch (Exception)
            {
                resultData = new { NotificationType = "error", NotificationMsg = "Error occurred while deleting state!" };
            }
            return Json(resultData, JsonRequestBehavior.AllowGet);
        }

    }
}