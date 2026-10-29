using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMWebConsole.Domain
{
    public class PhoneNumber : IAuditable
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public string PhoneNumber1 { get; set; }
        public string PhoneNumber2 { get; set; }
        public string PhoneNumber3 { get; set; }
        public int AuditId { get; set; }
        public int CreatedBy { get; set; }
        public int? LastModifiedBy { get; set; }
    }
}
