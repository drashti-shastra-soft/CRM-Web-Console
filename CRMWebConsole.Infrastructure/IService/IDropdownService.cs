using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMWebConsole.Infrastructure.IService
{
    public interface 
        IDropdownService
    {
        dynamic GetStatusOptions();

        dynamic GetPageSizeOptions(int selectedSize = 20);

        Task<dynamic> GetUserTypeOptionsAsync();

        Task<dynamic> GetClientTypeOptionsAsync();

        Task<dynamic> GetUserTypesAsync();

        Task<dynamic> GetDyeingSupervisorOptionsAsync();

        Task<dynamic> GetCollectionAgentOptionsAsync();

        dynamic PaymentPaymentTerms();

        Task<dynamic> GetBrokerOptionsAsync();

        Task<dynamic> GetStateOptionsAsync();

        Task<dynamic> FetchStateOptionsAsync();

        Task<dynamic> GetClientOptionAsync();

        Task<dynamic> GetItemOptions();

        Task<dynamic> GetCityOptionsAsync(int stateId);

        Task<dynamic> GetMarketOptionsAsync();

        Task<dynamic> GetCityOptionsAsync(string state, string city = null);

        dynamic GetAccountType();

        Task<dynamic> GetBankOptionsAsync();

        dynamic GetPaymentMethods();

        dynamic GetDyeingSupervisorStatusOptions();
    }
}
