using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMWebConsole.Infrastructure.IService
{
    public interface IInvoiceLineItemService
    {
        Task<List<Domain.InvoiceLineItem>> GetByAsync(int id);
    }
}
