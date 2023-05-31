using App.Contracts.DAL.Identity;
using Base.Contracts.DAL;

namespace App.Contracts.DAL;

public interface IAppUnitOfWork : IUnitOfWork
{
    IAppUserRepository AppUsers { get; }
    ICountryRepository Countries { get; }
    IFeedbackRepository Feedbacks { get; }
    IInvoiceRepository Invoices { get; }
    IOrderRepository Orders { get; }
    IPaymentTypeRepository PaymentTypes { get; }
    IPcRepository Pcs { get; }
    IProductRepository Products { get; }
    IProductInOrderRepository ProductInOrders { get; }
    IProductInPcRepository ProductInPcs { get; }
    IProductTypeRepository ProductTypes { get; }
    IServiceRepository Services { get; }
    IServiceTypeRepository ServiceTypes { get; }
    IShippingInfoRepository ShippingInfos { get; }
    IShippingTypeRepository ShippingTypes { get; }
    ISpecificationRepository Specifications { get; }
    ISpecificationTypeRepository SpecificationTypes { get; }
    IRefreshTokenRepository RefreshTokens { get; }
    
}