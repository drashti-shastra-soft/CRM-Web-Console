using CRMWebConsole.Infrastructure.IService;
using CRMWebConsole.Infrastructure;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Configuration;

namespace CRMWebConsole.Service
{
    public class ReceiptAllocationService : IReceiptAllocationService
    {
        private readonly IHttpClientService _httpClientService;
        private readonly string _baseUrl;
        private readonly string _token;
        private readonly int _loginUserId;

        public ReceiptAllocationService(IHttpClientService httpClientService)
        {
            _httpClientService = httpClientService;
            _baseUrl = ConfigurationManager.AppSettings.Get("APIBaseUrl");

            if (Infrastructure.HttpContextHelper.LoginUser != null)
            {
                _token = Infrastructure.HttpContextHelper.LoginUser.Token;
                _loginUserId = Infrastructure.HttpContextHelper.LoginUser.Id;
            }
        }

        public async Task<List<Domain.ReceiptAllocation>> GetByAsync(int id)
        {
            return await _httpClientService.GetAsync<List<Domain.ReceiptAllocation>>(
            url: $"{_baseUrl}/ReceiptAllocation/{id}",
                token: _token
            ).ConfigureAwait(false);
        }

        public async Task<Domain.ReceiptAllocation> CreateAsync(Domain.ReceiptAllocation mReceiptAllocation)
        {
            return await _httpClientService.PostAsync<Domain.ReceiptAllocation>(
                    url: $"{_baseUrl}/ReceiptAllocation",
                    obj: mReceiptAllocation,
                    token: _token
            ).ConfigureAwait(false);
        }


    }
}
