using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMWebConsole.Infrastructure.IService
{
    public interface IStateService
    {
        Task<Domain.StateLister> GetAllAsync(Domain.StateLister mLister);

        Task<Domain.State> CreateAsync(Domain.State mState);

        Task<Domain.State> GetAsync(int id);

        Task DeleteAsync(int id);
    }
}
