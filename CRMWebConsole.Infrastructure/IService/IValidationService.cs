using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMWebConsole.Infrastructure.IService
{
    public interface IValidationService
    {
        List<Domain.Invoice> Initiate(List<Domain.Invoice> mInvoices);
    }
}
