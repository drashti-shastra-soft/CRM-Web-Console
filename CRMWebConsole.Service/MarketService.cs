using CRMWebConsole.Infrastructure.IService;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMWebConsole.Service
{
    public class MarketService : IMarketService
    {
        private readonly IHttpClientService _httpClientService;
        private readonly string _baseUrl;
        private readonly string _token;
        private readonly int _loginUserId;

        public MarketService(IHttpClientService httpClientService)
        {
            _httpClientService = httpClientService;
            _baseUrl = ConfigurationManager.AppSettings.Get("APIBaseUrl");

            if (Infrastructure.HttpContextHelper.LoginUser != null)
            {
                _token = Infrastructure.HttpContextHelper.LoginUser.Token;
                _loginUserId = Infrastructure.HttpContextHelper.LoginUser.Id;
            }
        }

        public async Task<Domain.MarketLister> GetAllAsync(Domain.MarketLister mLister)
        {
            var result = await _httpClientService.PostAsync<Domain.MarketLister>(
                    url: $"{_baseUrl}/Market/GetAll",
                    obj: mLister,
                    token: _token
            ).ConfigureAwait(false);

            return result;
        }

        public async Task<Domain.Market> CreateAsync(Domain.Market mMarket)
        {
            return await _httpClientService.PostAsync<Domain.Market>(
                    url: $"{_baseUrl}/Market",
                    obj: mMarket,
                    token: _token
            ).ConfigureAwait(false);
        }

        public async Task<Domain.Market> GetAsync(int id)
        {
            var result = await _httpClientService.GetAsync<Domain.Market>(
                  url: $"{_baseUrl}/Market/{id}",
                  token: _token
            ).ConfigureAwait(false);
            return result;
        }

        public async Task DeleteAsync(int id)
        {
            await _httpClientService.DeleteAsync(
                    url: $"{_baseUrl}/Market/{id}/LoginUser/{_loginUserId}",
                    token: _token
            ).ConfigureAwait(false);
        }

        public async Task<bool> CheckRankExistAsync(Domain.Market mMarket)
        {
            var isExist = await _httpClientService.PostAsync<Domain.Market, bool>(
                url: $"{_baseUrl}/Market/CheckRankExist",
                obj: mMarket,
                token: _token
                ).ConfigureAwait(false);
            return isExist;
        }
    }

}
