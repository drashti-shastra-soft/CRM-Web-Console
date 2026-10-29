using CRMWebConsole.Infrastructure.IService;
using System.Threading.Tasks;
using System.Configuration;

namespace CRMWebConsole.Service
{
    public class CityService : ICityService
    {
        private readonly IHttpClientService _httpClientService;
        private readonly string _baseUrl;
        private readonly string _token;
        private readonly int _loginUserId;

        public CityService(IHttpClientService httpClientService)
        {
            _httpClientService = httpClientService;
            _baseUrl = ConfigurationManager.AppSettings.Get("APIBaseUrl");

            if (Infrastructure.HttpContextHelper.LoginUser != null)
            {
                _token = Infrastructure.HttpContextHelper.LoginUser.Token;
                _loginUserId = Infrastructure.HttpContextHelper.LoginUser.Id;
            }

        }

        public async Task<Domain.CityLister> GetAllAsync(Domain.CityLister mLister)
        {
            var result = await _httpClientService.PostAsync<Domain.CityLister>(
                    url: $"{_baseUrl}/City/GetAll",
                    obj: mLister,
                    token: _token
            ).ConfigureAwait(false);
            return result;
        }

        public async Task<Domain.City> CreateAsync(Domain.City mCity)
        {
            return await _httpClientService.PostAsync<Domain.City>(
                    url: $"{_baseUrl}/City",
                    obj: mCity,
                    token: _token
            ).ConfigureAwait(false);
        }

        public async Task<Domain.City> GetAsync(int id)
        {
            var result = await _httpClientService.GetAsync<Domain.City>(
                  url: $"{_baseUrl}/City/{id}",
                  token: _token
            ).ConfigureAwait(false);
            return result;
        }

        public async Task DeleteAsync(int id)
        {
            await _httpClientService.DeleteAsync(
                    url: $"{_baseUrl}/City/{id}/LoginUser/{_loginUserId}",
                    token: _token
            ).ConfigureAwait(false);
        }

    }
}
