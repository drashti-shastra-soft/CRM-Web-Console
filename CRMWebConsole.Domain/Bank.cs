using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMWebConsole.Domain
{
    public class Bank : IAuditable
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string AccountType { get; set; }
        public string AccountNumber { get; set; }
        public string BranchName { get; set; }
        public string IFSCode { get; set; }
        public string UPI { get; set; }
        public string Status { get; set; }
        public bool IsActive { get; set; } = true;
        public int CreatedBy { get; set; }
        public int? LastModifiedBy { get; set; }
    }
}
