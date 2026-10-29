using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMWebConsole.Infrastructure.IService
{
    public interface ISalesReportService
    {
        Task<Domain.SalesReportLister> GetAllAsync(Domain.SalesReportLister mLister);
    }
}
