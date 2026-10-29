using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMWebConsole.Infrastructure.IService
{
    public interface IOutstandingService
    {
        Task<Domain.OutstandingLister> GetAllAsync(Domain.OutstandingLister mLister);
    }
}
