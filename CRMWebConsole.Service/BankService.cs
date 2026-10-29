using CRMWebConsole.Infrastructure.IService;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMWebConsole.Service
{
    public class BankService : IBankService
    {
        private readonly IHttpClientService _httpClientService;
        private readonly string _baseUrl;
        private readonly string _token;
        private readonly int _loginUserId;

        public BankService(IHttpClientService httpClientService)
        {
            _httpClientService = httpClientService;
            _baseUrl = ConfigurationManager.AppSettings.Get("APIBaseUrl");

            if (Infrastructure.HttpContextHelper.LoginUser != null)
            {
                _token = Infrastructure.HttpContextHelper.LoginUser.Token;
                _loginUserId = Infrastructure.HttpContextHelper.LoginUser.Id;
            }
        }

        public async Task<Domain.BankLister> GetAllAsync(Domain.BankLister mLister)
        {
            var result = await _httpClientService.PostAsync<Domain.BankLister>(
                    url: $"{_baseUrl}/Bank/GetAll",
                    obj: mLister,
                    token: _token
            ).ConfigureAwait(false);

            return result;
        }

        public async Task<Domain.Bank> CreateAsync(Domain.Bank mBank)
        {
            return await _httpClientService.PostAsync<Domain.Bank>(
                    url: $"{_baseUrl}/Bank",
                    obj: mBank,
                    token: _token
            ).ConfigureAwait(false);
        }

        public async Task<Domain.Bank> GetAsync(int id)
        {
            var result = await _httpClientService.GetAsync<Domain.Bank>(
                  url: $"{_baseUrl}/Bank/{id}",
                  token: _token
            ).ConfigureAwait(false);
            return result;
        }

        public async Task DeleteAsync(int id)
        {
            await _httpClientService.DeleteAsync(
                    url: $"{_baseUrl}/Bank/{id}/LoginUser/{_loginUserId}",
                    token: _token
            ).ConfigureAwait(false);
        }
    }
}
