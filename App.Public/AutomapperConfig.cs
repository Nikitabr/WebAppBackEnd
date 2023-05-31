using App.Public.DTO.v1;
using AutoMapper;

namespace App.Public;

public class AutomapperConfig : Profile
{
    public AutomapperConfig()
    {
        CreateMap<App.Public.DTO.v1.Country, App.BLL.DTO.Country>().ReverseMap();
        CreateMap<App.Public.DTO.v1.Feedback, App.BLL.DTO.Feedback>().ReverseMap();
        CreateMap<App.Public.DTO.v1.Invoice, App.BLL.DTO.Invoice>().ReverseMap();
        CreateMap<App.Public.DTO.v1.Order, App.BLL.DTO.Order>().ReverseMap();
        CreateMap<App.Public.DTO.v1.PaymentType, App.BLL.DTO.PaymentType>().ReverseMap();
        CreateMap<App.Public.DTO.v1.Pc, App.BLL.DTO.Pc>().ReverseMap();
        CreateMap<App.Public.DTO.v1.Product, App.BLL.DTO.Product>().ReverseMap();
        CreateMap<App.Public.DTO.v1.ProductInPc, App.BLL.DTO.ProductInPc>().ReverseMap();
        CreateMap<App.Public.DTO.v1.ProductType, App.BLL.DTO.ProductType>().ReverseMap();
        CreateMap<App.Public.DTO.v1.Service, App.BLL.DTO.Service>().ReverseMap();
        CreateMap<App.Public.DTO.v1.ServiceType, App.BLL.DTO.ServiceType>().ReverseMap();
        CreateMap<App.Public.DTO.v1.ShippingInfo, App.BLL.DTO.ShippingInfo>().ReverseMap();
        CreateMap<App.Public.DTO.v1.ShippingType, App.BLL.DTO.ShippingType>().ReverseMap();
        CreateMap<App.Public.DTO.v1.Specification, App.BLL.DTO.Specification>().ReverseMap();
        CreateMap<App.Public.DTO.v1.SpecificationType, App.BLL.DTO.SpecificationType>().ReverseMap();
        CreateMap<App.Public.DTO.v1.Identity.AppUser, App.BLL.DTO.Identity.AppUser>().ReverseMap();
        CreateMap<App.Public.DTO.v1.Identity.AppRole, App.BLL.DTO.Identity.AppRole>().ReverseMap();
        CreateMap<App.Public.DTO.v1.Identity.RefreshToken, App.BLL.DTO.Identity.RefreshToken>().ReverseMap();
        CreateMap<App.Public.DTO.v1.ProductInOrder, BLL.DTO.ProductInOrder>().ReverseMap();
    }
}