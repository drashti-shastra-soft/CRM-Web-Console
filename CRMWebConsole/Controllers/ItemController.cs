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
    public class ItemController : Controller
    {

        private readonly IItemService _service;
        private readonly IDropdownService _dropdownService;

        public ItemController(IItemService service, IDropdownService dropdownService)
        {
            _service = service;
            _dropdownService = dropdownService;
        }

        public ActionResult Index()
        {
            return View(new Domain.ItemLister());
        }

        public ActionResult List(Domain.ItemLister mLister)
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
            return PartialView("_Create", new Domain.Item());
        }

        [HttpPost]
        public ActionResult Create(Domain.Item mItem)
        {
            try
            {
                System.Threading.Thread.Sleep(1000);
                mItem = StaticMethods.SetAuditOnUpsert(mItem);
                mItem = _service.CreateAsync(mItem).GetAwaiter().GetResult();

                if (mItem != null && mItem.Id > 0)
                {
                    ViewBag.NotificationType = "success";
                    ViewBag.NotificationMsg = "Item has been saved successfully!";
                }
                else
                {
                    ViewBag.NotificationType = "error";
                    ViewBag.NotificationMsg = "Error occurred while saving Item!";
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

            return PartialView("_Create", mItem);
        }

        [HttpGet]
        public ActionResult Get(int id)
        {
            var mItem = new Domain.Item();
            try
            {
                System.Threading.Thread.Sleep(1000);
                mItem = _service.GetAsync(id).GetAwaiter().GetResult();
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

            return PartialView("_Create", mItem);
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            dynamic resultData = new ExpandoObject();

            try
            {
                System.Threading.Thread.Sleep(1000);
                _service.DeleteAsync(id).GetAwaiter().GetResult();
                resultData = new { NotificationType = "success", NotificationMsg = "Item has been deleted!" };
            }
            catch (Exception)
            {
                resultData = new { NotificationType = "error", NotificationMsg = "Error occurred while deleting Item!" };
            }

            return Json(resultData, JsonRequestBehavior.AllowGet);
        }


    }


}