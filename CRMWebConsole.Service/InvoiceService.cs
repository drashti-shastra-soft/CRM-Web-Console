using CRMWebConsole.Infrastructure.IService;
using System.Collections.Generic;
using System.Configuration;
using System.Threading.Tasks;

namespace CRMWebConsole.Service
{
    public class InvoiceService : IInvoiceService
    {
        private readonly IHttpClientService _httpClientService;
        private readonly string _baseUrl;
        private readonly string _token;
        private readonly int _loginUserId;

        public InvoiceService(IHttpClientService httpClientService)
        {
            _httpClientService = httpClientService;
            _baseUrl = ConfigurationManager.AppSettings.Get("APIBaseUrl");

            if (Infrastructure.HttpContextHelper.LoginUser != null)
            {
                _token = Infrastructure.HttpContextHelper.LoginUser.Token;
                _loginUserId = Infrastructure.HttpContextHelper.LoginUser.Id;
            }
        }
        public async Task<Domain.InvoiceLister> GetAllAsync(Domain.InvoiceLister mLister)
        {
            var result = await _httpClientService.PostAsync<Domain.InvoiceLister>(
                url: $"{_baseUrl}/Invoice/GetAll",
                obj: mLister,
                token: _token
            ).ConfigureAwait(false);
            return result;
        }

        public async Task<Domain.Invoice> CreateAsync(Domain.Invoice mInvoice)
        {
            var result = await _httpClientService.PostAsync<Domain.Invoice>(
                url: $"{_baseUrl}/Invoice",
                obj: mInvoice,
                token: _token
            ).ConfigureAwait(false);
            return result;
        }

        public async Task<Domain.Invoice> GetAsync(int id)
        {
            var result = await _httpClientService.GetAsync<Domain.Invoice>(
                  url: $"{_baseUrl}/Invoice/{id}",
                  token: _token
            ).ConfigureAwait(false);
            return result;
        }

        public async Task DeleteAsync(int id)
        {
            await _httpClientService.DeleteAsync(
                url: $"{_baseUrl}/Invoice/{id}/LoginUser/{_loginUserId}",
                token: _token
            ).ConfigureAwait(false);
        }

        public async Task<List<Domain.Invoice>> GetBy(int clientId)
        {
            var result = await _httpClientService.GetAsync<List<Domain.Invoice>>(
                  url: $"{_baseUrl}/Invoice/Client/{clientId}",
                  token: _token
            ).ConfigureAwait(false);
            return result;
        }

    }
}
