using CRMWebConsole.Filter;
using CRMWebConsole.Helper;
using CRMWebConsole.Infrastructure;
using CRMWebConsole.Infrastructure.IService;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace CRMWebConsole.Controllers
{
    [Authentication]
    public class InvoiceController : Controller
    {
        private readonly IInvoiceService _service;
        private readonly IDropdownService _dropdownService;
        private readonly IFileReaderService _fileReaderService;
        private readonly IValidationService _validationService;
        private readonly IInvoiceLineItemService _invoiceLineService;

        public InvoiceController(IInvoiceService service, IDropdownService dropdownService, IFileReaderService fileReaderService, IValidationService validationService, IInvoiceLineItemService invoiceLineService)
        {
            _service = service;
            _dropdownService = dropdownService;
            _fileReaderService = fileReaderService;
            _validationService = validationService;
            _invoiceLineService = invoiceLineService;
        }

        public ActionResult Index()
        {
            return View(new Domain.InvoiceLister());
        }

        public ActionResult List(Domain.InvoiceLister mLister)
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
            }
            return PartialView("_List", mLister);
        }

        [HttpGet]
        public ActionResult Import()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Import(HttpPostedFileBase file)
        {
            var mInvoices = new List<Domain.Invoice>();
            if (file != null && file.ContentLength > 0)
            {
                string folderPath = Path.Combine(Server.MapPath("~/Uploads/Invoices"));
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }
                var filePath = Path.Combine(Server.MapPath("~/Uploads/Invoices"), $"{StaticMethods.GenerateGUID(15)}{Path.GetExtension(file.FileName)}");
                file.SaveAs(filePath);
                mInvoices = _fileReaderService.ReadInvoices(filePath);
                mInvoices = _validationService.Initiate(mInvoices);
            }
            return Json(mInvoices, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public ActionResult Create(Domain.Invoice mInvoice)
        {
            dynamic resultData = new ExpandoObject();
            try
            {
                System.Threading.Thread.Sleep(1000);
                mInvoice = StaticMethods.SetAuditOnUpsert(mInvoice);
                mInvoice = _service.CreateAsync(mInvoice).GetAwaiter().GetResult();
                if (mInvoice != null && mInvoice.Id > 0)
                    resultData = new { type = "success" };
                else
                    resultData = new { type = "error" };
            }
            catch (BadRequestException ex)
            {
                resultData = new { type = "error", message = ex.Message };
            }
            catch (Exception)
            {
                resultData = new { type = "error", message = "Internal server error!" };
            }
            return Json(resultData, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Get(int id)
        {
            var mInvoice = new Domain.Invoice();
            try
            {
                System.Threading.Thread.Sleep(1000);
                mInvoice = _service.GetAsync(id).GetAwaiter().GetResult();
            }
            catch (Exception)
            {
                ViewBag.NotificationType = "error";
                ViewBag.NotificationMsg = "Internal server error!";
            }
            finally
            {
                ViewBag.Clients = _dropdownService.GetClientOptionAsync().GetAwaiter().GetResult();
                ViewBag.Methods = _dropdownService.GetPaymentMethods();
            }
            return PartialView("_Create", mInvoice);
        }


        [HttpGet]
        public ActionResult Delete(int id)
        {
            dynamic resultData = new ExpandoObject();

            try
            {
                System.Threading.Thread.Sleep(1000);
                _service.DeleteAsync(id).GetAwaiter().GetResult();
                resultData = new { NotificationType = "success", NotificationMsg = "Invoice has been deleted!" };
            }
            catch (Exception)
            {
                resultData = new { NotificationType = "error", NotificationMsg = "Error occurred while deleting invoice!" };
            }

            return Json(resultData, JsonRequestBehavior.AllowGet);
        }


        [HttpGet, Route("GetInvoiceLine")]
        public ActionResult GetInvoiceLine(int id)
        {
            var mInvoiceLine = new List<Domain.InvoiceLineItem>();
            try
            {
                Thread.Sleep(1000);
                mInvoiceLine = _invoiceLineService.GetByAsync(id).GetAwaiter().GetResult();
            }
            catch (Exception)
            {
                ViewBag.NotificationType = "error";
                ViewBag.NotificationMsg = "Internal Server Error";
            }
            return PartialView("_InvoiceLine", mInvoiceLine);
        }

    }
}