using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMWebConsole.Infrastructure
{
    public static class Enums
    {
        public enum InvoiceColumn
        {
            [Display(Name = "Date")]
            Date = 1,
            [Display(Name = "Book Type")]
            Book_Type = 2,
            [Display(Name = "Client Name")]
            Client_Name = 3,
            [Display(Name = "Invoice Number")]
            Invoice_Number = 4,
            [Display(Name = "Challan Number")]
            Challan_Number = 5,
            [Display(Name = "LR Number")]
            LR_Number = 6,
            [Display(Name = "Vehicle Number")]
            Vehicle_Number = 7,
            [Display(Name = "Dyeing Supervisor")]
            Dyeing_Supervisor = 8,
            [Display(Name = "Collection Agent")]
            Collection_Agent = 9,
            [Display(Name = "Broker")]
            Broker = 10,
            [Display(Name = "Item Name")]
            Item_Name = 11,
            [Display(Name = "Style")]
            Style = 12,
            [Display(Name = "Lot Number")]
            Lot_Number = 13,
            [Display(Name = "Grey Meter")]
            Grey_Meter = 14,
            [Display(Name = "Piece")]
            Piece = 15,
            [Display(Name = "Part Piece")]
            Part_Piece = 16,
            [Display(Name = "Grey Value")]
            Grey_Value = 17,
            [Display(Name = "Finish Meter")]
            Finish_Meter = 18,
            [Display(Name = "Shortage")]
            Shortage = 19,
            [Display(Name = "Rate")]
            Rate = 20,
            [Display(Name = "Amount")]
            Amount = 21,
            [Display(Name = "Add To Total")]
            Add_To_Total = 22,
            [Display(Name = "Subtract From Total")]
            Subtract_From_Total = 23,
            [Display(Name = "Taxable Value")]
            Taxable_Value = 24,
            [Display(Name = "SGST")]
            SGST = 25,
            [Display(Name = "CGST")]
            CGST = 26,
            [Display(Name = "IGST")]
            IGST = 27,
            [Display(Name = "Net Amount")]
            Net_Amount = 28,
        }

        public enum UserType
        {
            Collection_Agent = 1
        }
    }
}
