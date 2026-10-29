using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMWebConsole.Infrastructure.IService
{
    public interface IFileReaderService
    {
        List<Domain.Invoice> ReadInvoices(string filePath);
    }
}
