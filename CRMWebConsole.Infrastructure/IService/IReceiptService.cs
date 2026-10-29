using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMWebConsole.Infrastructure.IService
{
    public interface IReceiptService
    {
        Task<Domain.ReceiptLister> GetAllAsync(Domain.ReceiptLister mLister);

        Task<Domain.Receipt> CreateAsync(Domain.Receipt mReceipt);

        Task<Domain.Receipt> GetAsync(int id);

        Task DeleteAsync(int id);

        Task PatchReceiptStatus(int id, string status);
    }
}
