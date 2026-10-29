using CRMWebConsole.Filter;
using CRMWebConsole.Helper;
using CRMWebConsole.Infrastructure;
using CRMWebConsole.Infrastructure.IService;
using CRMWebConsole.Service;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRMWebConsole.Controllers
{
    [Authentication]
    public class BrokerController : Controller
    {
        private readonly IBrokerService _service;
        private readonly IDropdownService _dropdownService;

        public BrokerController(IBrokerService service, IDropdownService dropdownService)
        {
            _service = service;
            _dropdownService = dropdownService;
        }

        public ActionResult Index()
        {
            return View(new Domain.BrokerLister());
        }

        public ActionResult List(Domain.BrokerLister mLister)
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
            return PartialView("_Create", new Domain.Broker());
        }

        [HttpPost]
        public ActionResult Create(Domain.Broker mBroker)
        {
            try
            {
                System.Threading.Thread.Sleep(1000);
                mBroker = StaticMethods.SetAuditOnUpsert(mBroker);
                mBroker = _service.CreateAsync(mBroker).GetAwaiter().GetResult();

                if (mBroker != null && mBroker.Id > 0)
                {
                    ViewBag.NotificationType = "success";
                    ViewBag.NotificationMsg = "Broker has been saved successfully!";
                }
                else
                {
                    ViewBag.NotificationType = "error";
                    ViewBag.NotificationMsg = "Error occurred while saving broker!";
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

            return PartialView("_Create", mBroker);
        }

        [HttpGet]
        public ActionResult Get(int id)
        {
            var mBroker = new Domain.Broker();
            try
            {
                System.Threading.Thread.Sleep(1000);
                mBroker = _service.GetAsync(id).GetAwaiter().GetResult();
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

            return PartialView("_Create", mBroker);
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            dynamic resultData = new ExpandoObject();

            try
            {
                System.Threading.Thread.Sleep(1000);
                _service.DeleteAsync(id).GetAwaiter().GetResult();
                resultData = new { NotificationType = "success", NotificationMsg = "Broker has been deleted!" };
            }
            catch (Exception)
            {
                resultData = new { NotificationType = "error", NotificationMsg = "Error occurred while deleting broker!" };
            }

            return Json(resultData, JsonRequestBehavior.AllowGet);
        }
    }
}