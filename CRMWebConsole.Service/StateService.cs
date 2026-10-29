using CRMWebConsole.Infrastructure.IService;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMWebConsole.Service
{
    public class StateService : IStateService
    {
        private readonly IHttpClientService _httpClientService;
        private readonly string _baseUrl;
        private readonly string _token;
        private readonly int _loginUserId;

        public StateService(IHttpClientService httpClientService)
        {
            _httpClientService = httpClientService;
            _baseUrl = ConfigurationManager.AppSettings.Get("APIBaseUrl");

            if (Infrastructure.HttpContextHelper.LoginUser != null)
            {
                _token = Infrastructure.HttpContextHelper.LoginUser.Token;
                _loginUserId = Infrastructure.HttpContextHelper.LoginUser.Id;
            }
        }

        public async Task<Domain.StateLister> GetAllAsync(Domain.StateLister mLister)
        {
            var result = await _httpClientService.PostAsync<Domain.StateLister>(
                    url: $"{_baseUrl}/State/GetAll",
                    obj: mLister,
                    token: _token
            ).ConfigureAwait(false);

            return result;
        }

        public async Task<Domain.State> CreateAsync(Domain.State mState)
        {
            return await _httpClientService.PostAsync<Domain.State>(
                    url: $"{_baseUrl}/State",
                    obj: mState,
                    token: _token
            ).ConfigureAwait(false);
        }

        public async Task<Domain.State> GetAsync(int id)
        {
            var result = await _httpClientService.GetAsync<Domain.State>(
                  url: $"{_baseUrl}/State/{id}",
                  token: _token
            ).ConfigureAwait(false);
            return result;
        }

        public async Task DeleteAsync(int id)
        {
            await _httpClientService.DeleteAsync(
                    url: $"{_baseUrl}/State/{id}/LoginUser/{_loginUserId}",
                    token: _token
            ).ConfigureAwait(false);
        }
    }
}
