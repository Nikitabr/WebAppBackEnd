using App.Contracts.BLL.Services;
using App.Contracts.BLL.Services.Identity;
using Base.Contracts.BLL;

namespace App.Contracts.BLL;

public interface IAppBLL : IBLL
{
    IAppUserService AppUsers { get; }
    ICountryService Countries { get; }
    IFeedbackService Feedbacks { get; }
    IInvoiceService Invoices { get; }
    IOrderService Orders { get; }
    IPaymentTypeService PaymentTypes { get; }
    IPcService Pcs { get; }
    IProductService Products { get; }
    IProductInOrderService ProductInOrders { get; }
    IProductInPcService ProductInPcs { get; }
    IProductTypeService ProductTypes { get; }
    IServiceService Services { get; }
    IServiceTypeService ServiceTypes { get; }
    IShippingInfoService ShippingInfos { get; }
    IShippingTypeService ShippingTypes { get; }
    ISpecificationService Specifications { get; }
    ISpecificationTypeService SpecificationTypes { get; }
    IRefreshTokenService RefreshTokens{ get; }
}