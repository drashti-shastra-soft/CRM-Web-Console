using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMWebConsole.Domain
{
    public class SalesReportLister
    {
        public List<SalesReport> List { get; set; } = new List<SalesReport>();

        public SalesReport SearchCriteria { get; set; } = new SalesReport();
        public Pagination Pagination { get; set; } = new Pagination();
    }
}
