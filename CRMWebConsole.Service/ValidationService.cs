using CRMWebConsole.Infrastructure;
using CRMWebConsole.Infrastructure.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace CRMWebConsole.Service
{
    public class ValidationService : IValidationService
    {
        private readonly IDropdownService _dropdownService;

        public ValidationService(IDropdownService dropdownService)
        {
            _dropdownService = dropdownService;
        }

        public List<Domain.Invoice> Initiate(List<Domain.Invoice> mInvoices)
        {
            if (mInvoices != null && mInvoices.Count > 0)
            {
                var mDyeingSupervisors = (List<SelectListItem>)_dropdownService.GetDyeingSupervisorOptionsAsync().GetAwaiter().GetResult();
                var mCollectionAgents = (List<SelectListItem>)_dropdownService.GetCollectionAgentOptionsAsync().GetAwaiter().GetResult();
                var mClients = (List<SelectListItem>)_dropdownService.GetClientOptionAsync().GetAwaiter().GetResult();
                var mItems = (List<SelectListItem>)_dropdownService.GetItemOptions().GetAwaiter().GetResult();
                var mBrokers = (List<SelectListItem>)_dropdownService.GetBrokerOptionsAsync().GetAwaiter().GetResult();

                foreach (var mInvoice in mInvoices)
                {
                    if (mInvoice.RawDate.IsNullOrEmpty())
                        mInvoice.Validation.Add("Date is required!");

                    if(mInvoice.BookType.IsNullOrEmpty())
                        mInvoice.Validation.Add("Book Type is required!");

                    if (mInvoice.ClientName.IsNullOrEmpty())
                        mInvoice.Validation.Add("Client Name is required!");
                    else if (mInvoice.ClientName.IsNotNullOrEmpty())
                    {
                        var mClient = mClients.Where(x => x.Text == mInvoice.ClientName).FirstOrDefault();
                        if (mClient != null && mClient.Value.IsNotNullOrEmpty())
                            mInvoice.ClientId = mClient.Value.ToInt();
                        else
                            mInvoice.Validation.Add($"{mInvoice.ClientName} is not configured as client!");
                    }


                    if (mInvoice.InvoiceNumber.IsNullOrEmpty())
                        mInvoice.Validation.Add("Invoice Number is required!");

                    if(mInvoice.VehicleNumber.IsNullOrEmpty())
                        mInvoice.Validation.Add("Vehicle Number should required!");

                  
                    if(mInvoice.DyeingSupervisorName.IsNullOrEmpty())
                        mInvoice.Validation.Add("Dyeing Supervisor Name is required!");
                    else if(mInvoice.DyeingSupervisorName.IsNotNullOrEmpty())
                    {
                        var mDyeingSupervisor = mDyeingSupervisors.Where(x => x.Text == mInvoice.DyeingSupervisorName).FirstOrDefault();
                        if (mDyeingSupervisor != null && mDyeingSupervisor.Value.IsNotNullOrEmpty())
                            mInvoice.DyeingSupervisorId = mDyeingSupervisor.Value.ToInt();
                        else
                            mInvoice.Validation.Add($"{mInvoice.DyeingSupervisorName} is not configured as dyeing supervisor!");
                    }

                    if(mInvoice.CollectionAgentName.IsNullOrEmpty())
                        mInvoice.Validation.Add("Collection Agent Name is required!");
                    else if(mInvoice.CollectionAgentName.IsNotNullOrEmpty())
                    {
                        var mCollectionAgent = mCollectionAgents.Where(x => x.Text == mInvoice.CollectionAgentName).FirstOrDefault();
                        if (mCollectionAgent != null && mCollectionAgent.Value.IsNotNullOrEmpty())
                            mInvoice.CollectionAgentId = mCollectionAgent.Value.ToInt();
                        else
                            mInvoice.Validation.Add($"{mInvoice.CollectionAgentName} is not configured as collection agent!");
                    }

                    if (mInvoice.TaxableValue <= default(decimal))
                        mInvoice.Validation.Add("Taxable Value is required!");

                    if (mInvoice.NetAmount <= default(decimal))
                        mInvoice.Validation.Add("Net Amount is required!");

                    foreach (var mLineItem in mInvoice.LineItems)
                    {

                        if (mLineItem.ItemName.IsNullOrEmpty())
                            mLineItem.Validation.Add("Item Name is required!");
                        else if (mLineItem.ItemName.IsNotNullOrEmpty())
                        {
                            var mItem = mItems.Where(x => x.Text == mLineItem.ItemName).FirstOrDefault();
                            if (mItem != null && mItem.Value.IsNotNullOrEmpty())
                                mLineItem.ItemId = mItem.Value.ToInt();
                            else
                                mLineItem.Validation.Add($"{mLineItem.ItemName} is not configured as Item Name!");
                        }

                        if (mLineItem.LotNumber.IsNullOrEmpty())
                            mLineItem.Validation.Add("Lot Number is required!");

                        if (mLineItem.GreyMeter <= default(decimal))
                            mLineItem.Validation.Add("Grey meter is required");

                        if (mLineItem.Piece <= default(decimal))
                            mLineItem.Validation.Add("Piece is required");

                        if (mLineItem.FinishMeter <= default(decimal))
                            mLineItem.Validation.Add("Finish Meter is required");

                        if (mLineItem.GreyValue <= default(decimal))
                            mLineItem.Validation.Add("Grey Value is required");

                        if (mLineItem.Rate <= default(decimal))
                            mLineItem.Validation.Add("Rate is required");

                        if (mLineItem.Amount <= default(decimal))
                            mLineItem.Validation.Add("Amount is required");
                    }
                }
            }

            return mInvoices;
        }
    }
}
