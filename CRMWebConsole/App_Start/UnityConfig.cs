using CRMWebConsole.Infrastructure;
using CRMWebConsole.Infrastructure.IService;
using CRMWebConsole.Service;
using System.Web.Mvc;
using Unity;
using Unity.Mvc5;

namespace CRMWebConsole
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();

            container.RegisterType<IAddressService, AddressService>();
            container.RegisterType<IAuthenticationService, AuthenticationService>();
            container.RegisterType<IBankService, BankService>();
            container.RegisterType<IBrokerService, BrokerService>();
            container.RegisterType<IChangePasswordService, ChangePasswordService>();
            container.RegisterType<ICityService, CityService>();
            container.RegisterType<IClientService, ClientService>();
            container.RegisterType<ICSVReaderService, CSVReaderService>();
            container.RegisterType<IDropdownService, DropdownService>();
            container.RegisterType<IFileReaderService, FileReaderService>();
            container.RegisterType<IForgotPasswordService, ForgotPasswordService>();
            container.RegisterType<IHttpClientService, HttpClientService>();
            container.RegisterType<IInvoiceLineItemService, InvoiceLineItemService>();
            container.RegisterType<IInvoiceService, InvoiceService>();
            container.RegisterType<IItemService, ItemService>();
            container.RegisterType<IMarketService, MarketService>();
            container.RegisterType<IOutstandingService, OutstandingService>();
            container.RegisterType<IPhoneNumberService, PhoneNumberService>();
            container.RegisterType<IReceiptAllocationService, ReceiptAllocationService>();
            container.RegisterType<IReceiptService, ReceiptService>();
            container.RegisterType<ISalesReceiptService, SalesReceiptService>();
            container.RegisterType<IStateService, StateService>();
            container.RegisterType<ISalesReportService, SalesReportService>();
            container.RegisterType<IUserService, UserService>();
            container.RegisterType<IUserTypeService, UserTypeService>();
            container.RegisterType<IValidationService, ValidationService>();


            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}