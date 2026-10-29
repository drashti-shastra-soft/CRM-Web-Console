using CRMWebConsole.Infrastructure.IService;
using System.Configuration;
using System.Threading.Tasks;

namespace CRMWebConsole.Service
{
    public class ReceiptService : IReceiptService
    {
        private readonly IHttpClientService _httpClientService;
        private readonly string _baseUrl;
        private readonly string _token;
        private readonly int _loginUserId;

        public ReceiptService(IHttpClientService httpClientService)
        {
            _httpClientService = httpClientService;
            _baseUrl = ConfigurationManager.AppSettings.Get("APIBaseUrl");

            if (Infrastructure.HttpContextHelper.LoginUser != null)
            {
                _token = Infrastructure.HttpContextHelper.LoginUser.Token;
                _loginUserId = Infrastructure.HttpContextHelper.LoginUser.Id;
            }
        }

        public async Task<Domain.ReceiptLister> GetAllAsync(Domain.ReceiptLister mLister)
        {
            var result = await _httpClientService.PostAsync<Domain.ReceiptLister>(
                url: $"{_baseUrl}/Receipt/GetAll",
                obj: mLister,
                token: _token
            ).ConfigureAwait(false);

            return result;
        }

        public async Task<Domain.Receipt> CreateAsync(Domain.Receipt mReceipt)
        {
            return await _httpClientService.PostAsync<Domain.Receipt>(
                url: $"{_baseUrl}/Receipt",
                obj: mReceipt,
                token: _token
            ).ConfigureAwait(false);
        }

        public async Task<Domain.Receipt> GetAsync(int id)
        {
            var result = await _httpClientService.GetAsync<Domain.Receipt>(
               url: $"{_baseUrl}/Receipt/{id}",
               token: _token
            ).ConfigureAwait(false);
            return result;
        }

        public async Task DeleteAsync(int id)
        {
            await _httpClientService.DeleteAsync(
                url: $"{_baseUrl}/Receipt/{id}/LoginUser/{_loginUserId}",
                token: _token
            ).ConfigureAwait(false);
        }

        public async Task PatchReceiptStatus(int id, string status)
        {
            await _httpClientService.PatchAsync(
                    url: $"{_baseUrl}/Receipt/{id}/Status/{status}/LoginUser/{_loginUserId}",
                    token: _token
            ).ConfigureAwait(false);
        }

    }
}
