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
    public class ClientController : Controller
    {
        private readonly IClientService _service;
        private readonly IDropdownService _dropdownService;
        private readonly IMarketService _marketService;
        private readonly IAddressService _addressService;
        private readonly IPhoneNumberService _phoneNumberService;

        public ClientController(IClientService service, IDropdownService dropdownService, IMarketService marketService, IAddressService addressService, IPhoneNumberService phoneNumberService)
        {
            _service = service;
            _dropdownService = dropdownService;
            _marketService = marketService;
            _addressService = addressService;
            _phoneNumberService = phoneNumberService;
        }

        public ActionResult Index()
        {
            return View(new Domain.ClientLister());
        }

        public ActionResult List(Domain.ClientLister mLister)
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
                ViewBag.UserType = _dropdownService.GetUserTypesAsync().GetAwaiter().GetResult();
                ViewBag.Status = _dropdownService.GetStatusOptions();
                ViewBag.DyeingSupervisors = _dropdownService.GetDyeingSupervisorOptionsAsync().GetAwaiter().GetResult();
                ViewBag.Broker = _dropdownService.GetBrokerOptionsAsync().GetAwaiter().GetResult();
                ViewBag.Pagination = _dropdownService.GetPageSizeOptions(mLister.Pagination.PageSize);
            }
            return PartialView("_List", mLister);
        }

        #region Client
        [HttpPost]
        public ActionResult SaveClient(Domain.Client mClient)
        {
            try
            {
                System.Threading.Thread.Sleep(1000);
                mClient = StaticMethods.SetAuditOnUpsert(mClient);
                mClient = _service.CreateAsync(mClient).GetAwaiter().GetResult();

                if (mClient != null && mClient.Id > 0)
                {
                    ViewBag.NotificationType = "success";
                    ViewBag.NotificationMsg = "Client has been saved successfully!";
                }
                else
                {
                    ViewBag.NotificationType = "error";
                    ViewBag.NotificationMsg = "Error occurred while saving client!";
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
                ViewBag.UserType = _dropdownService.GetUserTypesAsync().GetAwaiter().GetResult();
                ViewBag.DyeingSupervisors = _dropdownService.GetDyeingSupervisorOptionsAsync().GetAwaiter().GetResult();
                ViewBag.CollectionAgents = _dropdownService.GetCollectionAgentOptionsAsync().GetAwaiter().GetResult();
                ViewBag.ClientTypes = _dropdownService.GetClientTypeOptionsAsync().GetAwaiter().GetResult();
                ViewBag.PaymentTerms = _dropdownService.PaymentPaymentTerms();
                ViewBag.Market = _dropdownService.GetMarketOptionsAsync().GetAwaiter().GetResult();
                ViewBag.Broker = _dropdownService.GetBrokerOptionsAsync().GetAwaiter().GetResult();
                ViewBag.States = _dropdownService.GetStateOptionsAsync().GetAwaiter().GetResult();
                ViewBag.Cities = _dropdownService.GetCityOptionsAsync(0).GetAwaiter().GetResult();
            }

            return PartialView("_Client", mClient);
        }

        [HttpGet]
        public ActionResult GetClient(int id)
        {
            var mClient = new Domain.Client();
            try
            {
                System.Threading.Thread.Sleep(1000);
                mClient = _service.GetAsync(id).GetAwaiter().GetResult();

                if (string.IsNullOrEmpty(mClient.PaymentTerms))
                    mClient.PaymentTerms = "Net 30 Days";
            }
            catch (Exception)
            {
                ViewBag.NotificationType = "error";
                ViewBag.NotificationMsg = "Internal server error!";
            }
            finally
            {
                ViewBag.Status = _dropdownService.GetStatusOptions();
                ViewBag.UserType = _dropdownService.GetUserTypesAsync().GetAwaiter().GetResult();
                ViewBag.DyeingSupervisors = _dropdownService.GetDyeingSupervisorOptionsAsync().GetAwaiter().GetResult();
                ViewBag.CollectionAgents = _dropdownService.GetCollectionAgentOptionsAsync().GetAwaiter().GetResult();
                ViewBag.ClientTypes = _dropdownService.GetClientTypeOptionsAsync().GetAwaiter().GetResult();
                ViewBag.Broker = _dropdownService.GetBrokerOptionsAsync().GetAwaiter().GetResult();
                ViewBag.PaymentTerms = new SelectList(_dropdownService.PaymentPaymentTerms(), "Value", "Text");
                ViewBag.Market = _dropdownService.GetMarketOptionsAsync().GetAwaiter().GetResult();
                ViewBag.States = _dropdownService.GetStateOptionsAsync().GetAwaiter().GetResult();
            }
            return PartialView("_Client", mClient);
        }

        [HttpGet]
        public ActionResult DeleteClient(int id)
        {
            dynamic resultData = new ExpandoObject();

            try
            {
                System.Threading.Thread.Sleep(1000);
                _service.DeleteAsync(id).GetAwaiter().GetResult();
                resultData = new { NotificationType = "success", NotificationMsg = "Client has been deleted!" };
            }
            catch (Exception)
            {
                resultData = new { NotificationType = "error", NotificationMsg = "Error occurred while deleting client!" };
            }

            return Json(resultData, JsonRequestBehavior.AllowGet);
        } 

        #endregion

        #region Address

        [HttpGet]
        public ActionResult GetAddress(int id)
        {
            var mAddress = new Domain.Address();
            try
            {
                mAddress = _addressService.GetAsync(id).GetAwaiter().GetResult();
                mAddress = mAddress ?? new Domain.Address();
                mAddress.ClientId = id;
            }
            catch (Exception)
            {
                ViewBag.NotificationType = "error";
                ViewBag.NotificationMsg = "Internal server error!";
            }
            finally
            {
                ViewBag.Markets = _dropdownService.GetMarketOptionsAsync().GetAwaiter().GetResult();
                ViewBag.States = _dropdownService.GetStateOptionsAsync().GetAwaiter().GetResult();
                ViewBag.Cities = null;
                if (mAddress.Id > 0)
                    ViewBag.Cities = _dropdownService.GetCityOptionsAsync(mAddress.State, mAddress.City).GetAwaiter().GetResult();
            }
            return PartialView("_Address", mAddress);
        }

        [HttpGet]
        public JsonResult GetAddressByMarket(int marketId)
        {
            try
            {
                var market = _marketService.GetAsync(marketId).GetAwaiter().GetResult();
                if (market != null)
                {
                    return Json(new
                    {
                        success = true,
                        Line1 = market.Line1,
                        Line2 = market.Line2,
                        Line3 = market.Line3,
                        City = market.City,
                        State = market.State,
                        ZipCode = market.ZipCode
                    }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception)
            {
                throw;
            }

            return Json(new { success = false, message = "Unable to fetch market address." }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult SaveAddress(Domain.Address mAddress)
        {
            try
            {
                System.Threading.Thread.Sleep(1000);
                mAddress = StaticMethods.SetAuditOnUpsert(mAddress);
                mAddress = _addressService.CreateAsync(mAddress).GetAwaiter().GetResult();

                if (mAddress != null && mAddress.Id > 0)
                {
                    ViewBag.NotificationType = "success";
                    ViewBag.NotificationMsg = "Address has been saved successfully!";
                }
                else
                {
                    ViewBag.NotificationType = "error";
                    ViewBag.NotificationMsg = "Error occurred while saving Address!";
                }
            }
            catch (Exception)
            {
                ViewBag.NotificationType = "error";
                ViewBag.NotificationMsg = "Internal server error!";
            }
            return PartialView("_Address", mAddress);
        }

        #endregion

        #region City

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

        #endregion

        #region Phone Number

        [HttpGet]
        public ActionResult GetPhoneNumber(int id)
        {
            var mPhoneNumber = new Domain.PhoneNumber();
            try
            {
                mPhoneNumber = _phoneNumberService.GetAsync(id).GetAwaiter().GetResult();
                mPhoneNumber = mPhoneNumber ?? new Domain.PhoneNumber();
                mPhoneNumber.ClientId = id;
            }
            catch (Exception)
            {
                ViewBag.NotificationType = "error";
                ViewBag.NotificationMsg = "Internal server error!";
            }
            return PartialView("_PhoneNumber", mPhoneNumber);
        }

        [HttpPost]
        public ActionResult SavePhoneNumber(Domain.PhoneNumber mPhoneNumber)
        {
            try
            {
                System.Threading.Thread.Sleep(1000);
                mPhoneNumber = StaticMethods.SetAuditOnUpsert(mPhoneNumber);
                mPhoneNumber = _phoneNumberService.CreateAsync(mPhoneNumber).GetAwaiter().GetResult();

                if (mPhoneNumber != null && mPhoneNumber.Id > 0)
                {
                    ViewBag.NotificationType = "success";
                    ViewBag.NotificationMsg = "Number has been saved successfully!";
                }
                else
                {
                    ViewBag.NotificationType = "error";
                    ViewBag.NotificationMsg = "Error occurred while saving Number!";
                }
            }

            catch (Exception)
            {
                ViewBag.NotificationType = "error";
                ViewBag.NotificationMsg = "Internal server error!";
            }
            return PartialView("_PhoneNumber", mPhoneNumber);
        }

        #endregion
    }
}