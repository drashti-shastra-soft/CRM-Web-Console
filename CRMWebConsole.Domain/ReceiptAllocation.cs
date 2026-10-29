using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMWebConsole.Domain
{
    public class ReceiptAllocation
    {
        public int Id { get; set; }
        public int ReceiptId { get; set; }
        public int InvoiceId { get; set; }
        public string InvoiceNumber { get; set; }
        public decimal NetAmount { get; set; }
        public decimal AmountDue { get; set; }
        public decimal Amount { get; set; }
        public string Status { get; set; }
    }
}
