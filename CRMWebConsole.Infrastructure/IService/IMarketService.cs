using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMWebConsole.Infrastructure.IService
{
    public interface IMarketService
    {
        Task<Domain.MarketLister> GetAllAsync(Domain.MarketLister mLister);

        Task<Domain.Market> CreateAsync(Domain.Market mMarket);

        Task<Domain.Market> GetAsync(int id);

        Task DeleteAsync(int id);

        Task<bool> CheckRankExistAsync(Domain.Market mMarket);
    }
}
