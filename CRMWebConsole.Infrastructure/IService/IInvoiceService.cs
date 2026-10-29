using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMWebConsole.Infrastructure.IService
{
    public interface IInvoiceService
    {
        Task<Domain.InvoiceLister> GetAllAsync(Domain.InvoiceLister mLister);

        Task<Domain.Invoice> CreateAsync(Domain.Invoice mInvoice);

        Task<Domain.Invoice> GetAsync(int id);

        Task<List<Domain.Invoice>> GetBy(int clientId);

        Task DeleteAsync(int id);
    }
}
