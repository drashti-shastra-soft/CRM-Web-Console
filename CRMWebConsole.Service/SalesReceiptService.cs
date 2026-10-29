using CRMWebConsole.Infrastructure.IService;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMWebConsole.Service
{
    public class SalesReceiptService : ISalesReceiptService
    {
        private readonly IHttpClientService _httpClientService;
        private readonly string _baseUrl;
        private readonly string _token;
        private readonly int _loginUserId;

        public SalesReceiptService(IHttpClientService httpClientService)
        {
            _httpClientService = httpClientService;
            _baseUrl = ConfigurationManager.AppSettings.Get("APIBaseUrl");

            if (Infrastructure.HttpContextHelper.LoginUser != null)
            {
                _token = Infrastructure.HttpContextHelper.LoginUser.Token;
                _loginUserId = Infrastructure.HttpContextHelper.LoginUser.Id;
            }
        }

        public async Task<Domain.SalesReceiptLister> GetAllAsync(Domain.SalesReceiptLister mLister)
        {
            var result = await _httpClientService.PostAsync<Domain.SalesReceiptLister>(
                    url: $"{_baseUrl}/SalesReceipt/GetAll",
                    obj: mLister,
                    token: _token
            ).ConfigureAwait(false);
            return result;
        }
    }
}
