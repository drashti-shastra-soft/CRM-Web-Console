using CRMWebConsole.Infrastructure.IService;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMWebConsole.Service
{
    public class InvoiceLineItemService : IInvoiceLineItemService
    {
        private readonly IHttpClientService _httpClientService;
        private readonly string _baseUrl;
        private readonly string _token;
        private readonly int _loginUserId;

        public InvoiceLineItemService(IHttpClientService httpClientService)
        {
            _httpClientService = httpClientService;
            _baseUrl = ConfigurationManager.AppSettings.Get("APIBaseUrl");

            if (Infrastructure.HttpContextHelper.LoginUser != null)
            {
                _token = Infrastructure.HttpContextHelper.LoginUser.Token;
                _loginUserId = Infrastructure.HttpContextHelper.LoginUser.Id;
            }
        }

        public async Task<List<Domain.InvoiceLineItem>> GetByAsync(int id)
        {
            return await _httpClientService.GetAsync<List<Domain.InvoiceLineItem>>(
            url: $"{_baseUrl}/InvoiceLineItem/{id}",
                token: _token
            ).ConfigureAwait(false);
        }
    }
}
