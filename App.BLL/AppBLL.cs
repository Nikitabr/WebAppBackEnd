using App.BLL.Mappers;
using App.BLL.Mappers.Identity;
using App.BLL.Services;
using App.BLL.Services.Identity;
using App.Contracts.BLL;
using App.Contracts.BLL.Services;
using App.Contracts.BLL.Services.Identity;
using App.Contracts.DAL;
using App.Domain.Identity;
using App.Public.DTO.v1.Identity;
using App.Resources.App.Domain;
using AutoMapper;
using Base.BLL;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace App.BLL;

public class AppBLL: BaseBLL<IAppUnitOfWork>, IAppBLL
{
    protected IAppUnitOfWork UnitOfWork;
    private readonly AutoMapper.IMapper _mapper;
    
    public AppBLL(IAppUnitOfWork unitOfWork, IMapper mapper)
    {
        UnitOfWork = unitOfWork;
        _mapper = mapper;
    }
    
    public override async Task<int> SaveChangesAsync()
    {
        return await UnitOfWork.SaveChangesAsync();
    }

    public override int SaveChanges()
    {
        return UnitOfWork.SaveChanges();
    }

    private IAppUserService? _appUsers;

    public IAppUserService AppUsers =>
        _appUsers ??= new AppUserService(UnitOfWork.AppUsers, new AppUserMapper(_mapper));

    private ICountryService? _countries;

    public ICountryService Countries =>
        _countries ??= new CountryService(UnitOfWork.Countries, new CountryMapper(_mapper));


    private IFeedbackService? _feedbacks;
    
    public IFeedbackService Feedbacks =>
        _feedbacks ??= new FeedbackService(UnitOfWork.Feedbacks, new FeedbackMapper(_mapper));

    private IInvoiceService? _invoices;

    public IInvoiceService Invoices =>
        _invoices ??= new InvoiceService(UnitOfWork.Invoices, new InvoiceMapper(_mapper));

    private IOrderService? _orders;
    
    public IOrderService Orders =>
        _orders ??= new OrderService(UnitOfWork.Orders, new OrderMapper(_mapper));

    private IPaymentTypeService? _paymentTypes;

    public IPaymentTypeService PaymentTypes =>
        _paymentTypes ??= new PaymentTypeService(UnitOfWork.PaymentTypes, new PaymentTypeMapper(_mapper));
    
    
    private IPcService? _pcs;
    
    public IPcService Pcs => 
        _pcs ??= new PcService(UnitOfWork.Pcs, new PcMapper(_mapper));

    private IProductService? _products;
    
    public IProductService Products => 
        _products ??= new ProductService(UnitOfWork.Products, new ProductMapper(_mapper));

    private IProductInOrderService? _productInOrders;

    public IProductInOrderService ProductInOrders =>
        _productInOrders ??= new ProductInOrderService(UnitOfWork.ProductInOrders, new ProductInOrderMapper(_mapper));

    private IProductInPcService? _productInPcs;

    public IProductInPcService ProductInPcs =>
        _productInPcs ??= new ProductInPcService(UnitOfWork.ProductInPcs, new ProductInPcMapper(_mapper));

    private IProductTypeService? _productTypes;
    
    public IProductTypeService ProductTypes => 
        _productTypes ??= new ProductTypeService(UnitOfWork.ProductTypes, new ProductTypeMapper(_mapper));

    private IServiceService? _services;

    public IServiceService Services =>
        _services ??= new ServiceService(UnitOfWork.Services, new ServiceMapper(_mapper));
    
    private IServiceTypeService? _serviceTypes;

    public IServiceTypeService ServiceTypes =>
        _serviceTypes ??= new ServiceTypeService(UnitOfWork.ServiceTypes, new ServiceTypeMapper(_mapper));
    
    private IShippingInfoService? _shippingInfos;

    public IShippingInfoService ShippingInfos =>
        _shippingInfos ??= new ShippingInfoService(UnitOfWork.ShippingInfos, new ShippingInfoMapper(_mapper));
    
    private IShippingTypeService? _shippingTypes;

    public IShippingTypeService ShippingTypes =>
        _shippingTypes ??= new ShippingTypeService(UnitOfWork.ShippingTypes, new ShippingTypeMapper(_mapper));
    
    private ISpecificationService? _specifications;

    public ISpecificationService Specifications =>
        _specifications ??= new SpecificationService(UnitOfWork.Specifications, new SpecificationMapper(_mapper));
    
    private ISpecificationTypeService? _specificationTypes;

    public ISpecificationTypeService SpecificationTypes =>
        _specificationTypes ??= new SpecificationTypeService(UnitOfWork.SpecificationTypes, new SpecificationTypeMapper(_mapper));

    private IRefreshTokenService? _refreshTokens;
    
    public IRefreshTokenService RefreshTokens =>
        _refreshTokens ??= new RefreshTokenService(UnitOfWork.RefreshTokens, new RefreshTokenMapper(_mapper));
}