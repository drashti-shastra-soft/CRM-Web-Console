using CRMWebConsole.Infrastructure.IService;
using System.Threading.Tasks;
using System.Configuration;

namespace CRMWebConsole.Service
{
    public class PhoneNumberService : IPhoneNumberService
    {
        private readonly IHttpClientService _httpClientService;
        private readonly string _baseUrl;
        private readonly string _token;
        private readonly int _loginUserId;

        public PhoneNumberService(IHttpClientService httpClientService)
        {
            _httpClientService = httpClientService;
            _baseUrl = ConfigurationManager.AppSettings.Get("APIBaseUrl");

            if (Infrastructure.HttpContextHelper.LoginUser != null)
            {
                _token = Infrastructure.HttpContextHelper.LoginUser.Token;
                _loginUserId = Infrastructure.HttpContextHelper.LoginUser.Id;
            }
        }

        public async Task<Domain.PhoneNumber> CreateAsync(Domain.PhoneNumber mPhoneNumber)
        {
            return await _httpClientService.PostAsync<Domain.PhoneNumber>(
                    url: $"{_baseUrl}/PhoneNumber",
                    obj: mPhoneNumber,
                    token: _token
            ).ConfigureAwait(false);
        }

        public async Task<Domain.PhoneNumber> GetAsync(int id)
        {
            var result = await _httpClientService.GetAsync<Domain.PhoneNumber>(
                url: $"{_baseUrl}/PhoneNumber/{id}",
                token: _token
            ).ConfigureAwait(false);

            return result;
        }

    }
}
