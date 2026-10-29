using CRMWebConsole.Infrastructure.IService;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMWebConsole.Service
{
    public class UserTypeService : IUserTypeService
    {
        private readonly IHttpClientService _httpClientService;
        private readonly string _baseUrl;
        private readonly string _token;

        public UserTypeService(IHttpClientService httpClientService)
        {
            _httpClientService = httpClientService;
            _baseUrl = ConfigurationManager.AppSettings.Get("APIBaseUrl");

            if (Infrastructure.HttpContextHelper.LoginUser != null)
                _token = Infrastructure.HttpContextHelper.LoginUser.Token;
        }

        public async Task<Domain.UserType> GetAllAsync()
        {
            var result = await _httpClientService.PostAsync<Domain.UserType>(
                    url: $"{_baseUrl}/UserType/GetAll",
                    obj: new Domain.UserType(),
                    token: _token
            ).ConfigureAwait(false);
            return result;
        }
    }

}
