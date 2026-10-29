using CRMWebConsole.Domain;
using CRMWebConsole.Infrastructure;
using CRMWebConsole.Infrastructure.IService;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;


namespace CRMWebConsole.Service
{
    public class DropdownService : IDropdownService
    {

        #region Declaration

        private readonly IHttpClientService _httpClientService;
        private readonly string baseUrl;

        public DropdownService(IHttpClientService httpClientService)
        {
            _httpClientService = httpClientService;
            baseUrl = ConfigurationManager.AppSettings.Get("APIBaseUrl");
        }

        #endregion

        #region Status
        public dynamic GetStatusOptions()
        {
            return new List<SelectListItem>
            {
                new SelectListItem { Text = "Active", Value = "Active" },
                new SelectListItem { Text = "Disabled", Value = "Disabled" }
            };
        }
        #endregion

        #region Pagenation
        public dynamic GetPageSizeOptions(int selectedSize = 20)
        {
            var list = new List<SelectListItem>
            {
                new SelectListItem { Text = "5", Value = "5" },
                new SelectListItem { Text = "10", Value = "10" },
                new SelectListItem { Text = "20", Value = "20" },
                new SelectListItem { Text = "50", Value = "50" },
                new SelectListItem { Text = "100", Value = "100" },
                new SelectListItem { Text = "All", Value = "-1" }
            };

            list.ForEach(item => item.Selected = item.Value == selectedSize.ToString());
            return list;
        }

        #endregion

        #region User Type
        public async Task<dynamic> GetUserTypeOptionsAsync()
        {
            var mSelectListItems = new List<SelectListItem>();

            var mUserTypes = await _httpClientService.GetAsync<List<Domain.UserType>>(
                url: $"{baseUrl}/UserType",
                token: HttpContextHelper.LoginUser?.Token
            ).ConfigureAwait(false);

            foreach (var mUserType in mUserTypes)
            {
                mSelectListItems.Add(new SelectListItem() { Text = mUserType.Name, Value = Convert.ToString(mUserType.Id) });
            }
            return mSelectListItems;
        }
        #endregion

        #region Client Type
        public async Task<dynamic> GetClientTypeOptionsAsync()
        {
            var mSelectListItems = new List<SelectListItem>();

            var mClientTypes = await _httpClientService.GetAsync<List<Domain.ClientType>>(
                    url: $"{baseUrl}/ClientType/GetActiveClientTypes",
                    token: HttpContextHelper.LoginUser?.Token
            ).ConfigureAwait(false);

            foreach (var mClientType in mClientTypes)
            {
                mSelectListItems.Add(new SelectListItem() { Text = mClientType.Name, Value = Convert.ToString(mClientType.Id) });
            }
            return mSelectListItems;
        }

        #endregion

        #region User Types
        public async Task<dynamic> GetUserTypesAsync()
        {
            var mSelectListItems = new List<SelectListItem>();

            var mUserTypes = await _httpClientService.GetAsync<List<Domain.UserType>>(
                            url: $"{baseUrl}/UserType/GetActiveUserTypes",
                            token: HttpContextHelper.LoginUser?.Token
            ).ConfigureAwait(false);

            foreach (var mUserType in mUserTypes)
            {
                mSelectListItems.Add(new SelectListItem() { Text = mUserType.Name, Value = Convert.ToString(mUserType.Id) });
            }
            return mSelectListItems;
        }
        #endregion

        #region Dyeing Supervisor
        public async Task<dynamic> GetDyeingSupervisorOptionsAsync()
        {
            var mSelectListItems = new List<SelectListItem>();

            var mDyeingSupervisors = await _httpClientService.GetAsync<List<Domain.User>>(
                            url: $"{baseUrl}/User/GetActiveUsers/UserType/{4}",
                            token: HttpContextHelper.LoginUser?.Token
            ).ConfigureAwait(false);

            foreach (var mDyeingSupervisor in mDyeingSupervisors)
            {
                mSelectListItems.Add(new SelectListItem() { Text = $"{mDyeingSupervisor.FirstName} {mDyeingSupervisor.LastName}", Value = Convert.ToString(mDyeingSupervisor.Id) });
            }
            return mSelectListItems;
        }
        #endregion

        #region Item
        public async Task<dynamic> GetItemOptions()
        {
            var mSelectListItems = new List<SelectListItem>();
            var mItems = await _httpClientService.GetAsync<List<Domain.Item>>(
                url: $"{baseUrl}/Item",
                token: HttpContextHelper.LoginUser?.Token
            ).ConfigureAwait(false);

            foreach (var mItem in mItems)
            {
                mSelectListItems.Add(new SelectListItem() { Text = mItem.Name, Value = Convert.ToString(mItem.Id) });
            }
            return mSelectListItems;
        }
        #endregion

        #region Collection Agent
        public async Task<dynamic> GetCollectionAgentOptionsAsync()
        {
            var mSelectListItems = new List<SelectListItem>();

            var mCollectionAgents = await _httpClientService.GetAsync<List<Domain.User>>(
                            url: $"{baseUrl}/User/GetActiveUsers/UserType/{5}",
                            token: HttpContextHelper.LoginUser?.Token
            ).ConfigureAwait(false);

            foreach (var mCollectionAgent in mCollectionAgents)
            {
                mSelectListItems.Add(new SelectListItem() { Text = $"{mCollectionAgent.FirstName} {mCollectionAgent.LastName}", Value = Convert.ToString(mCollectionAgent.Id) });
            }
            return mSelectListItems;
        }

        #endregion

        #region Payment Payment

        public dynamic PaymentPaymentTerms()
        {
            return new List<SelectListItem>
            {
                    new SelectListItem { Text = "Due On Receipt", Value = "Due On Receipt" },
                    new SelectListItem { Text = "Net 15 Days", Value = "Net 15 Days" },
                    new SelectListItem { Text = "Net 30 Days", Value = "Net 30 Days" },
                    new SelectListItem { Text = "Net 45 Days", Value = "Net 45 Days" },
                    new SelectListItem { Text = "Net 60 Days", Value = "Net 60 Days" },
            };
        }

        #endregion

        #region Broker
        public async Task<dynamic> GetBrokerOptionsAsync()
        {
            var mSelectListItems = new List<SelectListItem>();

            var mBrokers = await _httpClientService.GetAsync<List<Domain.Broker>>(
                url: $"{baseUrl}/Broker",
                token: HttpContextHelper.LoginUser?.Token
            ).ConfigureAwait(false);

            foreach (var mBroker in mBrokers)
            {
                mSelectListItems.Add(new SelectListItem() { Text = mBroker.Name, Value = Convert.ToString(mBroker.Id) });
            }
            return mSelectListItems;
        }
        #endregion

        #region Client
        public async Task<dynamic> GetClientOptionAsync()
        {
            var mSelectListItems = new List<SelectListItem>();
            var mClients = await _httpClientService.GetAsync<List<Domain.Client>>(
                url: $"{baseUrl}/Client/GetActiveClient",
                token: HttpContextHelper.LoginUser?.Token
            ).ConfigureAwait(false);

            foreach (var mClient in mClients)
            {
                mSelectListItems.Add(new SelectListItem() { Text = mClient.Name, Value = Convert.ToString(mClient.Id) });
            }

            return mSelectListItems;
        }
        #endregion

        #region State
        public async Task<dynamic> GetStateOptionsAsync()
        {
            var mSelectListItems = new List<SelectListItem>();

            var mStates = await _httpClientService.GetAsync<List<Domain.State>>(
                    url: $"{baseUrl}/State/GetActiveStates",
                    token: HttpContextHelper.LoginUser?.Token
            ).ConfigureAwait(false);

            foreach (var mState in mStates)
            {
                mSelectListItems.Add(new SelectListItem() { Text = $"{mState.Code} - {mState.Name}", Value = mState.Name });
            }
            return mSelectListItems;
        }

        public async Task<dynamic> FetchStateOptionsAsync()
        {
            var mSelectListItems = new List<SelectListItem>();

            var mStates = await _httpClientService.GetAsync<List<Domain.State>>(
                url: $"{baseUrl}/State/GetActiveStates",
                token: HttpContextHelper.LoginUser?.Token
            ).ConfigureAwait(false);

            foreach (var mState in mStates)
            {
                mSelectListItems.Add(new SelectListItem() { Text = $"{mState.Code} - {mState.Name}", Value = Convert.ToString(mState.Id) });
            }
            return mSelectListItems;
        }
        #endregion

        #region City
        public async Task<dynamic> GetCityOptionsAsync(int stateId)
        {
            var mSelectListItems = new List<SelectListItem>();

            var mCites = await _httpClientService.GetAsync<List<Domain.City>>(
                    url: $"{baseUrl}/City/GetActiveBy/StateId/{stateId}",
                    token: HttpContextHelper.LoginUser?.Token
            ).ConfigureAwait(false);

            foreach (var mCity in mCites)
            {
                mSelectListItems.Add(new SelectListItem() { Text = mCity.Name, Value = mCity.Name });
            }
            return mSelectListItems;
        }

        public async Task<dynamic> GetCityOptionsAsync(string state, string city = null)
        {
            var mSelectListItems = new List<SelectListItem>();

            var mCites = await _httpClientService.GetAsync<List<Domain.City>>(
                    url: $"{baseUrl}/City/GetActiveBy/State/{state}",
                    token: HttpContextHelper.LoginUser?.Token
            ).ConfigureAwait(false);

            foreach (var mCity in mCites)
            {
                mSelectListItems.Add(new SelectListItem() { Text = mCity.Name, Value = mCity.Name });
            }

            if (city.IsNotNullOrEmpty())
                mSelectListItems.ForEach(item => item.Selected = item.Value == city);

            return mSelectListItems;

        }
        #endregion

        #region Market
        public async Task<dynamic> GetMarketOptionsAsync()
        {
            var mSelectListItems = new List<SelectListItem>();

            var mMarkets = await _httpClientService.GetAsync<List<Domain.Market>>(
                url: $"{baseUrl}/Market",
                token: HttpContextHelper.LoginUser?.Token
            ).ConfigureAwait(false);

            foreach (var mMarket in mMarkets)
            {
                mSelectListItems.Add(new SelectListItem() { Text = mMarket.Name, Value = Convert.ToString(mMarket.Id) });
            }
            return mSelectListItems;
        }
        #endregion

        #region AccountType
        public dynamic GetAccountType()
        {
            return new List<SelectListItem>
          {
                  new SelectListItem { Text = "Current Account", Value = "Current Account" },
                  new SelectListItem { Text = "Overdraft Account", Value = "Overdraft Account" },
                  new SelectListItem { Text = "Loan Account", Value = "Loan Account" },
          };
        }
        #endregion

        #region Bank
        public async Task<dynamic> GetBankOptionsAsync()
        {
            var mSelectListItems = new List<SelectListItem>();

            var mBanks = await _httpClientService.GetAsync<List<Domain.Bank>>(
                    url: $"{baseUrl}/Bank/GetActiveBank",
                    token: HttpContextHelper.LoginUser?.Token
            ).ConfigureAwait(false);

            foreach (var mBank in mBanks)
            {
                mSelectListItems.Add(new SelectListItem() { Text = mBank.Name, Value = Convert.ToString(mBank.Id) });
            }
            return mSelectListItems;
        }
        #endregion

        #region Method
        public dynamic GetPaymentMethods()
        {
            return new List<SelectListItem>
          {
                  new SelectListItem { Text = "Net Banking", Value = "Net Banking" },
                  new SelectListItem { Text = "NEFT", Value = "NEFT" },
                  new SelectListItem { Text = "RTGS", Value = "RTGS" },
                  new SelectListItem { Text = "IMPS", Value = "IMPS" },
                  new SelectListItem { Text = "Cash", Value = "Cash" },
                  new SelectListItem { Text = "UPI Payment", Value = "UPI Payment" },
                  new SelectListItem { Text = "Cheque Payment", Value = "Cheque Payment" },
                  new SelectListItem { Text = "Demand Draft", Value = "Demand Draft" },
          };
        }
        #endregion

        #region
        public dynamic GetDyeingSupervisorStatusOptions()
        {
            return new List<SelectListItem>
            {
                new SelectListItem { Text = "Approved", Value = "Approved" },
                new SelectListItem { Text = "Rejected", Value = "Rejected" }
            };
        }


        #endregion


    }
}
