using CRMWebConsole.Infrastructure.IService;
using CRMWebConsole.Infrastructure;
using System.Collections.Generic;
using CRMWebConsole.Filter;
using CRMWebConsole.Helper;
using System.Dynamic;
using System.Web.Mvc;
using System;

namespace CRMWebConsole.Controllers
{
    [Authentication]
    public class ReceiptController : Controller
    {
        private readonly IReceiptService _service;
        private readonly IDropdownService _dropdownService;
        private readonly IReceiptAllocationService _receiptAllocationService;
        private readonly IInvoiceService _invoiceService;

        public ReceiptController(IReceiptService service, IDropdownService dropdownService, IReceiptAllocationService receiptAllocationService, IInvoiceService invoiceService)
        {
            _service = service;
            _dropdownService = dropdownService;
            _receiptAllocationService = receiptAllocationService;
            _invoiceService = invoiceService;
        }

        public ActionResult Index()
        {
            return View(new Domain.ReceiptLister());
        }

        public ActionResult List(Domain.ReceiptLister mLister)
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
                ViewBag.CollectionAgents = _dropdownService.GetCollectionAgentOptionsAsync().GetAwaiter().GetResult();
            }
            return PartialView("_List", mLister);
        }

        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.Clients = _dropdownService.GetClientOptionAsync().GetAwaiter().GetResult();
            ViewBag.Methods = _dropdownService.GetPaymentMethods();
            ViewBag.CollectionAgents = _dropdownService.GetCollectionAgentOptionsAsync().GetAwaiter().GetResult();
            ViewBag.Banks = _dropdownService.GetBankOptionsAsync().GetAwaiter().GetResult();

            return PartialView("_Create", new Domain.Receipt());
        }

        [HttpPost]
        public ActionResult Create(Domain.Receipt mReceipt)
        {
            dynamic resultData = new ExpandoObject();
            try
            { 
                System.Threading.Thread.Sleep(1000);
                mReceipt = StaticMethods.SetAuditOnUpsert(mReceipt);
                mReceipt = _service.CreateAsync(mReceipt).GetAwaiter().GetResult();

                if (mReceipt != null && mReceipt.Id > 0)
                {
                    resultData = new { NotificationType = "success", NotificationMsg = "Receipt has been saved successfully!" };
                }
                else
                {
                    resultData = new { NotificationType = "error", NotificationMsg = "Error occurred while saving receipt!" };
                }
            }
            catch (Exception)
            {
                resultData = new { NotificationType = "error", NotificationMsg = "Internal server error!" };
            }
            finally
            {
                ViewBag.CollectionAgents = _dropdownService.GetCollectionAgentOptionsAsync().GetAwaiter().GetResult();
                ViewBag.Banks = _dropdownService.GetBankOptionsAsync().GetAwaiter().GetResult();
                ViewBag.DsvStatus = _dropdownService.GetDyeingSupervisorStatusOptions();
            }
            return Json(resultData, JsonRequestBehavior.AllowGet);
        }

        

        [HttpGet]
        public ActionResult Delete(int id)
        {
            dynamic resultData = new ExpandoObject();

            try
            {
                System.Threading.Thread.Sleep(1000);
                _service.DeleteAsync(id).GetAwaiter().GetResult();
                resultData = new { NotificationType = "success", NotificationMsg = "Receipt has been deleted!" };
            }
            catch (Exception)
            {
                resultData = new { NotificationType = "error", NotificationMsg = "Error occurred while deleting receipt!" };
            }

            return Json(resultData, JsonRequestBehavior.AllowGet);
        }

        #region Receipt Allocation

        [HttpGet]
        public ActionResult GetReceiptAllocation(int id)
        {
            var mReceiptAllocation = new List<Domain.ReceiptAllocation>();
            try
            {
                System.Threading.Thread.Sleep(1000);
                mReceiptAllocation = _receiptAllocationService.GetByAsync(id).GetAwaiter().GetResult();
            }
            catch (Exception)
            {
                ViewBag.NotificationType = "error";
                ViewBag.NotificationMsg = "Internal Server Error";
            }
            return PartialView("_ReceiptAllocation", mReceiptAllocation);

        }

        #endregion

        #region Get Invoice

        [HttpGet]
        public JsonResult GetInvoicesBy(int clientId)
        {
            var mInvoices = new List<Domain.Invoice>();
            try
            {
                System.Threading.Thread.Sleep(1000);
                mInvoices = _invoiceService.GetBy(clientId).GetAwaiter().GetResult();
            }
            catch (Exception)
            {
                throw;
            }
            return Json(mInvoices, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Dyeing Supervisor Status

        [HttpGet]
        public ActionResult Get(int id)
        {
            var mReceipt = new Domain.Receipt();
            try
            {
                System.Threading.Thread.Sleep(1000);
            }
            catch (Exception)
            {
                ViewBag.NotificationType = "error";
                ViewBag.NotificationMsg = "Internal server error!";
            }
            finally
            {
                ViewBag.DsvStatus = _dropdownService.GetDyeingSupervisorStatusOptions();
            }
            return PartialView("_Status", mReceipt);
        }

        [HttpPost]
        public ActionResult SaveStatus(int id, string status)
        {
            try
            {
                System.Threading.Thread.Sleep(1000);
                _service.PatchReceiptStatus(id, status).GetAwaiter().GetResult();
                ViewBag.NotificationType = "success";
                ViewBag.NotificationMsg = "Status has been saved successfully!";
            }

            catch (Exception)
            {
                ViewBag.NotificationType = "error";
                ViewBag.NotificationMsg = "Internal server error!";
            }
            finally
            {
                ViewBag.DsvStatus = _dropdownService.GetDyeingSupervisorStatusOptions();
            }

            return PartialView("_Status");
        }
        #endregion

    }
}   