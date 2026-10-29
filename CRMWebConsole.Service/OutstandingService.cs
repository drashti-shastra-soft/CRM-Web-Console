using CRMWebConsole.Infrastructure.IService;
using System.Threading.Tasks;
using System.Configuration;

namespace CRMWebConsole.Service
{
    public class OutstandingService : IOutstandingService
    {
        private readonly IHttpClientService _httpClientService;
        private readonly string _baseUrl;
        private readonly string _token;
        private readonly int _loginUserId;

        public OutstandingService(IHttpClientService httpClientService)
        {
            _httpClientService = httpClientService;
            _baseUrl = ConfigurationManager.AppSettings.Get("APIBaseUrl");

            if (Infrastructure.HttpContextHelper.LoginUser != null)
            {
                _token = Infrastructure.HttpContextHelper.LoginUser.Token;
                _loginUserId = Infrastructure.HttpContextHelper.LoginUser.Id;
            }
        }

        public async Task<Domain.OutstandingLister> GetAllAsync(Domain.OutstandingLister mLister)
        {
            var result = await _httpClientService.PostAsync<Domain.OutstandingLister>(
                    url: $"{_baseUrl}/Outstanding/GetAll",
                    obj: mLister,
                    token: _token
            ).ConfigureAwait(false);
            return result;
        }
    }
}
