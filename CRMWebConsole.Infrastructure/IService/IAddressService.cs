using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMWebConsole.Infrastructure.IService
{
    public interface IAddressService
    {
        Task<Domain.Address> CreateAsync(Domain.Address mAddress);
        Task<Domain.Address> GetAsync(int id);
        Task DeleteAsync(int id);
    }
}
