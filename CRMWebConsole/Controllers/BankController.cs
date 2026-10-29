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
    public class BankController : Controller
    {
        private readonly IBankService _service;
        private readonly IDropdownService _dropdownService;

        public BankController(IBankService service, IDropdownService dropdownService)
        {
            _service = service;
            _dropdownService = dropdownService;
        }

        public ActionResult Index()
        {
            return View(new Domain.BankLister());
        }

        public ActionResult List(Domain.BankLister mLister)
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
                ViewBag.AccountTypes = _dropdownService.GetAccountType();
                ViewBag.Pagination = _dropdownService.GetPageSizeOptions(mLister.Pagination.PageSize);
            }
            return PartialView("_List", mLister);
        }

        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.AccountTypes = _dropdownService.GetAccountType();
            return PartialView("_Create", new Domain.Bank());
        }

        [HttpPost]
        public ActionResult Create(Domain.Bank mBank)
        {
            try
            {
                System.Threading.Thread.Sleep(1000);
                mBank = StaticMethods.SetAuditOnUpsert(mBank);
                mBank = _service.CreateAsync(mBank).GetAwaiter().GetResult();

                if (mBank != null && mBank.Id > 0)
                {
                    ViewBag.NotificationType = "success";
                    ViewBag.NotificationMsg = "Bank has been saved successfully!";
                }
                else
                {
                    ViewBag.NotificationType = "error";
                    ViewBag.NotificationMsg = "Error occurred while saving Bank!";
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
                ViewBag.AccountTypes = _dropdownService.GetAccountType();
            }

            return PartialView("_Create", mBank);
        }

        [HttpGet]
        public ActionResult Get(int id)
        {
            var mBank = new Domain.Bank();
            try
            {
                System.Threading.Thread.Sleep(1000);
                mBank = _service.GetAsync(id).GetAwaiter().GetResult();
            }
            catch (Exception)
            {
                ViewBag.NotificationType = "error";
                ViewBag.NotificationMsg = "Internal server error!";
            }
            finally
            {
                ViewBag.Status = _dropdownService.GetStatusOptions();
                ViewBag.AccountTypes = _dropdownService.GetAccountType();
            }

            return PartialView("_Create", mBank);
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            dynamic resultData = new ExpandoObject();

            try
            {
                System.Threading.Thread.Sleep(1000);
                _service.DeleteAsync(id).GetAwaiter().GetResult();
                resultData = new { NotificationType = "success", NotificationMsg = "Bank has been deleted!" };
            }
            catch (Exception)
            {
                resultData = new { NotificationType = "error", NotificationMsg = "Error occurred while deleting Bank!" };
            }

            return Json(resultData, JsonRequestBehavior.AllowGet);
        }
    }
}