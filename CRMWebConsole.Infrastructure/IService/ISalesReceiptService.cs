using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMWebConsole.Infrastructure.IService
{
    public interface ISalesReceiptService
    {
        Task<Domain.SalesReceiptLister> GetAllAsync(Domain.SalesReceiptLister mLister);
    }
}
