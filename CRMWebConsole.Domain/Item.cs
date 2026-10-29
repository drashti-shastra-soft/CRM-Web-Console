using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMWebConsole.Domain
{
    public class Item : IAuditable
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; } = true;
        public int AuditId { get; set; }
        public int CreatedBy { get; set; }
        public int? LastModifiedBy { get; set; }
        public string Token { get; set; }
        public string Status { get; set; }
    }
}
