using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMWebConsole.Domain
{
    public class InvoiceLister
    {
        public List<Invoice> List { get; set; } = new List<Invoice>();

        public Invoice SearchCriteria { get; set; } = new Invoice();

        public Pagination Pagination { get; set; } = new Pagination();
    }
}
