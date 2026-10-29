using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMWebConsole.Infrastructure.IService
{
    public interface ICityService
    {
        Task<Domain.CityLister> GetAllAsync(Domain.CityLister mLister);

        Task<Domain.City> CreateAsync(Domain.City mCity);

        Task<Domain.City> GetAsync(int id);

        Task DeleteAsync(int id);
    }
}
