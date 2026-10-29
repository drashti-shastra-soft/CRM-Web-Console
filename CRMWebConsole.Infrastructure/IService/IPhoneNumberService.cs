using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMWebConsole.Infrastructure.IService
{
    public interface IPhoneNumberService
    {
        Task<Domain.PhoneNumber> GetAsync(int id);

        Task<Domain.PhoneNumber> CreateAsync(Domain.PhoneNumber mPhoneNumber);
    }
}
