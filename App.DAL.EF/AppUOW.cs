using App.Contracts.DAL;
using App.Contracts.DAL.Identity;
using App.DAL.DTO;
using App.DAL.EF.Mappers;
using App.DAL.EF.Mappers.Identity;
using App.DAL.EF.Repositories;
using App.DAL.EF.Repositories.Identity;
using App.Domain.Identity;
using Base.DAL;
using Base.DAL.EF;

namespace App.DAL.EF;

public class AppUOW : BaseUOW<AppDbContext>, IAppUnitOfWork
{
    private readonly AutoMapper.IMapper _mapper;

    public AppUOW(AppDbContext dbContext, AutoMapper.IMapper mapper) : base(dbContext)
    {
        _mapper = mapper;
    }


    private IAppUserRepository? _appUsers;

    public virtual IAppUserRepository AppUsers =>
        _appUsers ??= new AppUserRepository(UOWDbContext, new AppUserMapper(_mapper));


    private ICountryRepository? _countries;

    public virtual ICountryRepository Countries =>
        _countries ??= new CountryRepository(UOWDbContext, new CountryMapper(_mapper));

    private IFeedbackRepository? _feedbacks;

    public virtual IFeedbackRepository Feedbacks =>
        _feedbacks ??= new FeedbackRepository(UOWDbContext, new FeedbackMapper(_mapper));

    private IInvoiceRepository? _invoices;

    public virtual IInvoiceRepository Invoices =>
        _invoices ??= new InvoiceRepository(UOWDbContext, new InvoiceMapper(_mapper));

    private IOrderRepository? _orders;
    public virtual IOrderRepository Orders =>
        _orders ??= new OrderRepository(UOWDbContext, new OrderMapper(_mapper));


    private IPaymentTypeRepository? _paymentTypes;

    public virtual IPaymentTypeRepository PaymentTypes =>
        _paymentTypes ??= new PaymentTypeRepository(UOWDbContext, new PaymentTypeMapper(_mapper));
    
    private IPcRepository? _pcs;

    public virtual IPcRepository Pcs =>
        _pcs ??= new PcRepository(UOWDbContext, new PcMapper(_mapper));

    private IProductRepository? _products;
    public virtual IProductRepository Products =>
        _products ??= new ProductRepository(UOWDbContext, new ProductMapper(_mapper));

    private IProductInOrderRepository? _productInOrders;

    public virtual IProductInOrderRepository ProductInOrders =>
        _productInOrders ??= new ProductInOrderRepository(UOWDbContext, new ProductInOrderMapper(_mapper));
    
    private IProductInPcRepository? _productInPcs;

    public virtual IProductInPcRepository ProductInPcs =>
        _productInPcs ??= new ProductInPcRepository(UOWDbContext, new ProductInPcMapper(_mapper));
    
    private IProductTypeRepository? _productTypes;
    
    public virtual IProductTypeRepository ProductTypes =>
        _productTypes ??= new ProductTypeRepository(UOWDbContext, new ProductTypeMapper(_mapper));
    
    private IServiceRepository? _services;
    public virtual IServiceRepository Services =>
        _services ??= new ServiceRepository(UOWDbContext, new ServiceMapper(_mapper));
    
    
    private IServiceTypeRepository? _serviceTypes;
    public virtual IServiceTypeRepository ServiceTypes =>
        _serviceTypes ??= new ServiceTypeRepository(UOWDbContext, new ServiceTypeMapper(_mapper));
    
    
    private IShippingInfoRepository? _shippingInfos;
    public virtual IShippingInfoRepository ShippingInfos =>
        _shippingInfos ??= new ShippingInfoRepository(UOWDbContext, new ShippingInfoMapper(_mapper));
    
    
    private IShippingTypeRepository? _shippingTypes;
    public virtual IShippingTypeRepository ShippingTypes =>
        _shippingTypes ??= new ShippingTypeRepository(UOWDbContext, new ShippingTypeMapper(_mapper));
    
    
    private ISpecificationRepository? _specifications;
    public virtual ISpecificationRepository Specifications =>
        _specifications ??= new SpecificationRepository(UOWDbContext, new SpecificationMapper(_mapper));
    
    
    private ISpecificationTypeRepository? _specificationTypes;
    public virtual ISpecificationTypeRepository SpecificationTypes =>
        _specificationTypes ??= new SpecificationTypeRepository(UOWDbContext, new SpecificationTypeMapper(_mapper));

    private IRefreshTokenRepository? _refreshTokens;

    public virtual IRefreshTokenRepository RefreshTokens =>
        _refreshTokens ??= new RefreshTokenRepository(UOWDbContext, new RefreshTokenMapper(_mapper));
}