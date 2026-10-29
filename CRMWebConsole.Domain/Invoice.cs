using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMWebConsole.Domain
{
    public class Invoice : IAuditable
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string BookType { get; set; }
        public int ClientId { get; set; }
        public string ClientName { get; set; }
        public string InvoiceNumber { get; set; }
        public string ChallanNumber { get; set; }
        public string LRNumber { get; set; }
        public string VehicleNumber { get; set; }
        public int DyeingSupervisorId { get; set; }
        public string DyeingSupervisorName { get; set; }
        public int CollectionAgentId { get; set; }
        public string CollectionAgentName { get; set; }
        public int? BrokerId { get; set; }
        public string BrokerName { get; set; }
        public decimal TaxableValue { get; set; }
        public decimal? SGST { get; set; }
        public decimal? CGST { get; set; }
        public decimal? IGST { get; set; }
        public decimal NetAmount { get; set; }
        public string Status { get; set; }
        public decimal TotalPayment { get; set; }
        public string RawDate { get; set; }
        public string LotNumber { get; set; }
        public string Taka { get; set; }
        public string TP { get; set; }
        public decimal? AddAmount { get; set; }
        public decimal? LessAmount { get; set; }
            public int CreatedBy { get; set; }
            public int? LastModifiedBy { get; set; }
        public decimal AmountDue { get; set; }
        public List<Domain.InvoiceLineItem> LineItems { get; set; } = new List<Domain.InvoiceLineItem>();
        public List<string> Validation { get; set; } = new List<string>();
    }
}
