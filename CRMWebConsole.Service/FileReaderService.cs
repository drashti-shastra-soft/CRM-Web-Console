using CRMWebConsole.Infrastructure;
using CRMWebConsole.Infrastructure.Helper;
using CRMWebConsole.Infrastructure.IService;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace CRMWebConsole.Service
{
    public class FileReaderService : IFileReaderService
    {
        private readonly ICSVReaderService _csvReaderService;

        public FileReaderService(ICSVReaderService csvReaderService)
        {
            _csvReaderService = csvReaderService;
        }

        public List<Domain.Invoice> ReadInvoices(string filePath)
        {
            var mInvoices = new List<Domain.Invoice>();

            var dataTable = _csvReaderService.ReadFile(filePath);
            if (dataTable != null && dataTable.Rows != null && dataTable.Rows.Count > 0)
            {
                dataTable = Infrastructure.StaticMethods.RemoveEscapeSequences(dataTable);
                ValidateColumnName(dataTable);
                mInvoices = ConvertFromDataTableToModel(dataTable);
            }

            return mInvoices;
        }

        private void ValidateColumnName(DataTable dataTable)
        {
            if (dataTable != null && dataTable.Columns != null && dataTable.Columns.Count > 0)
            {
                var mColumns = EnumsHelper.GetEnumDisplayNames(new Enums.InvoiceColumn());
                foreach (DataColumn column in dataTable.Columns)
                {
                    if (mColumns.Where(x => x.Equals(column.ColumnName)).Count() == 0)
                        throw new InvalidOperationException($"Invalid Column Name '{column.ColumnName}'. Please correct them and upload file again.");
                }

                if (mColumns != null && mColumns.Count != dataTable.Columns.Count)
                    throw new InvalidOperationException($"Invalid import file format. Please correct them and upload file again");
            }
        }

        private List<Domain.Invoice> ConvertFromDataTableToModel(DataTable dataTable)
        {
            var mInvoices = new List<Domain.Invoice>();

            List<string> invoiceNumbers = dataTable.Rows.OfType<DataRow>().Select(dr => (string)dr[$"{Enums.InvoiceColumn.Invoice_Number.GetDisplayValue()}"]).Distinct().ToList();
            var searchColumnName = Enums.InvoiceColumn.Invoice_Number.ToString().Replace("_", " ");
            foreach (var invoiceNumber in invoiceNumbers)
            {
                var mInvoice = new Domain.Invoice();
                var dataRow = dataTable.Select($"[{searchColumnName}] = '{invoiceNumber}'").FirstOrDefault();
                mInvoice.RawDate = StaticMethods.GetValue(dataRow, Enums.InvoiceColumn.Date.GetDisplayValue()).ToTrim().ToDateString();
                mInvoice.BookType = StaticMethods.GetValue(dataRow, Enums.InvoiceColumn.Book_Type.GetDisplayValue()).ToTrim().ToDateString();
                mInvoice.ClientName = StaticMethods.GetValue(dataRow, Enums.InvoiceColumn.Client_Name.GetDisplayValue()).ToTrim();
                mInvoice.InvoiceNumber = StaticMethods.GetValue(dataRow, Enums.InvoiceColumn.Invoice_Number.GetDisplayValue()).ToTrim();
                mInvoice.ChallanNumber = StaticMethods.GetValue(dataRow, Enums.InvoiceColumn.Challan_Number.GetDisplayValue()).ToTrim();
                mInvoice.LRNumber = StaticMethods.GetValue(dataRow, Enums.InvoiceColumn.LR_Number.GetDisplayValue()).ToTrim();
                mInvoice.VehicleNumber = StaticMethods.GetValue(dataRow, Enums.InvoiceColumn.Vehicle_Number.GetDisplayValue()).ToTrim();
                mInvoice.DyeingSupervisorName = StaticMethods.GetValue(dataRow, Enums.InvoiceColumn.Dyeing_Supervisor.GetDisplayValue()).ToTrim();
                mInvoice.CollectionAgentName = StaticMethods.GetValue(dataRow, Enums.InvoiceColumn.Collection_Agent.GetDisplayValue()).ToTrim();
                mInvoice.BrokerName = StaticMethods.GetValue(dataRow, Enums.InvoiceColumn.Broker.GetDisplayValue()).ToTrim();
                mInvoice.TaxableValue = StaticMethods.GetValue(dataRow, Enums.InvoiceColumn.Taxable_Value.GetDisplayValue()).ToTrim().ToDecimal();
                mInvoice.SGST = StaticMethods.GetValue(dataRow, Enums.InvoiceColumn.SGST.GetDisplayValue()).ToTrim().ToDecimal();
                mInvoice.CGST = StaticMethods.GetValue(dataRow, Enums.InvoiceColumn.CGST.GetDisplayValue()).ToTrim().ToDecimal();
                mInvoice.IGST = StaticMethods.GetValue(dataRow, Enums.InvoiceColumn.IGST.GetDisplayValue()).ToTrim().ToDecimal();
                mInvoice.NetAmount = StaticMethods.GetValue(dataRow, Enums.InvoiceColumn.Net_Amount.GetDisplayValue()).ToTrim().ToDecimal();
                mInvoice.LineItems.AddRange(GetLineItems(dataTable, mInvoice.InvoiceNumber));
                mInvoices.Add(mInvoice);
            }

            return mInvoices;
        }

        private List<Domain.InvoiceLineItem> GetLineItems(DataTable dataTable, string billNumber)
        {
            var mLineItems = new List<Domain.InvoiceLineItem>();
            if (dataTable != null && billNumber.IsNotNullOrEmpty())
            {
                var dataRows = dataTable.Select($"[{Enums.InvoiceColumn.Invoice_Number.GetDisplayValue()}] = '" + billNumber + "'");
                if (dataRows != null)
                {
                    foreach (var dataRow in dataRows)
                    {
                        var mLineItem = new Domain.InvoiceLineItem();

                        mLineItem.ItemName = StaticMethods.GetValue(dataRow, Enums.InvoiceColumn.Item_Name.GetDisplayValue()).ToTrim();
                        mLineItem.Style = StaticMethods.GetValue(dataRow, Enums.InvoiceColumn.Style.GetDisplayValue()).ToTrim();
                        mLineItem.LotNumber = StaticMethods.GetValue(dataRow, Enums.InvoiceColumn.Lot_Number.GetDisplayValue()).ToTrim();
                        mLineItem.GreyMeter = StaticMethods.GetValue(dataRow, Enums.InvoiceColumn.Grey_Meter.GetDisplayValue()).ToTrim().ToDecimal();
                        mLineItem.Piece = StaticMethods.GetValue(dataRow, Enums.InvoiceColumn.Piece.GetDisplayValue()).ToTrim().ToTrim().ToInt();
                        mLineItem.PartPiece = StaticMethods.GetValue(dataRow, Enums.InvoiceColumn.Part_Piece.GetDisplayValue()).ToTrim().ToInt();
                        mLineItem.GreyValue = StaticMethods.GetValue(dataRow, Enums.InvoiceColumn.Grey_Value.GetDisplayValue()).ToTrim().ToDecimal();
                        mLineItem.FinishMeter = StaticMethods.GetValue(dataRow, Enums.InvoiceColumn.Finish_Meter.GetDisplayValue()).ToTrim().ToDecimal();
                        mLineItem.Shortage = StaticMethods.GetValue(dataRow, Enums.InvoiceColumn.Shortage.GetDisplayValue()).ToTrim().ToDecimal();
                        mLineItem.Rate = StaticMethods.GetValue(dataRow, Enums.InvoiceColumn.Rate.GetDisplayValue()).ToTrim().ToDecimal();
                        mLineItem.Amount = StaticMethods.GetValue(dataRow, Enums.InvoiceColumn.Amount.GetDisplayValue()).ToTrim().ToDecimal();
                        mLineItem.AddToTotal = StaticMethods.GetValue(dataRow, Enums.InvoiceColumn.Add_To_Total.GetDisplayValue()).ToTrim().ToDecimal();
                        mLineItem.SubtractFromTotal = StaticMethods.GetValue(dataRow, Enums.InvoiceColumn.Subtract_From_Total.GetDisplayValue()).ToTrim().ToDecimal();

                        mLineItems.Add(mLineItem);
                    }
                }
            }
            return mLineItems;
        }
    }
}
