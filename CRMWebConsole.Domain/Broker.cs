using System;
using System.Linq;

namespace CRMWebConsole.Domain
{
    public class Broker : IAuditable
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ContactPerson { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
        public string PANNumber { get; set; }
        public string AadharNumber { get; set; }
        public decimal Brokerage { get; set; }
        public bool IsActive { get; set; } = true;
        public int AuditId { get; set; }
        public int CreatedBy { get; set; }
        public int? LastModifiedBy { get; set; }
        public string Status { get; set; }
        public string Token { get; set; }
    }
}
