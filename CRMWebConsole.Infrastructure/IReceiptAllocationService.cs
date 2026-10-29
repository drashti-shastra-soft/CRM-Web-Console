using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMWebConsole.Infrastructure
{
    public interface IReceiptAllocationService
    {
        Task<List<Domain.ReceiptAllocation>> GetByAsync(int id);

        Task<Domain.ReceiptAllocation> CreateAsync(Domain.ReceiptAllocation mReceiptAllocation);
    }
}
