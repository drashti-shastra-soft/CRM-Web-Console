using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMWebConsole.Infrastructure.IService
{
    public interface IBankService
    {
        Task<Domain.BankLister> GetAllAsync(Domain.BankLister mLister);
        Task<Domain.Bank> CreateAsync(Domain.Bank mBank);
        Task<Domain.Bank> GetAsync(int id);
        Task DeleteAsync(int id);
    }
}
