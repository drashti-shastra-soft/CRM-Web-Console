using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMWebConsole.Infrastructure.IService
{
    public interface IBrokerService
    {
        Task<Domain.BrokerLister> GetAllAsync(Domain.BrokerLister mLister);

        Task<Domain.Broker> CreateAsync(Domain.Broker mBroker);

        Task<Domain.Broker> GetAsync(int id);

        Task DeleteAsync(int id);
    }
}
