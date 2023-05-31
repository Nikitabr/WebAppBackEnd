using App.DAL.DTO;
using AutoMapper;

namespace App.DAL.EF;

public class AutomapperConfig : Profile
{
    public AutomapperConfig()
    {
        CreateMap<App.DAL.DTO.Country, App.Domain.Country>().ReverseMap();
        CreateMap<App.DAL.DTO.Feedback, App.Domain.Feedback>().ReverseMap();
        CreateMap<App.DAL.DTO.Invoice, App.Domain.Invoice>().ReverseMap();
        CreateMap<App.DAL.DTO.Order, App.Domain.Order>().ReverseMap();
        CreateMap<App.DAL.DTO.PaymentType, App.Domain.PaymentType>().ReverseMap();
        CreateMap<App.DAL.DTO.Pc, App.Domain.Pc>().ReverseMap();
        CreateMap<App.DAL.DTO.Product, App.Domain.Product>().ReverseMap();
        CreateMap<App.DAL.DTO.ProductInPc, App.Domain.ProductInPc>().ReverseMap();
        CreateMap<App.DAL.DTO.ProductType, App.Domain.ProductType>().ReverseMap();
        CreateMap<App.DAL.DTO.Service, App.Domain.Service>().ReverseMap();
        CreateMap<App.DAL.DTO.ServiceType, App.Domain.ServiceType>().ReverseMap();
        CreateMap<App.DAL.DTO.ShippingInfo, App.Domain.ShippingInfo>().ReverseMap();
        CreateMap<App.DAL.DTO.ShippingType, App.Domain.ShippingType>().ReverseMap();
        CreateMap<App.DAL.DTO.Specification, App.Domain.Specification>().ReverseMap();
        CreateMap<App.DAL.DTO.SpecificationType, App.Domain.SpecificationType>().ReverseMap();
        CreateMap<App.DAL.DTO.Identity.RefreshToken, App.Domain.Identity.RefreshToken>().ReverseMap();
        CreateMap<App.DAL.DTO.Identity.AppUser, App.Domain.Identity.AppUser>().ReverseMap();
        CreateMap<App.DAL.DTO.Identity.AppRole, App.Domain.Identity.AppRole>().ReverseMap();
        CreateMap<App.DAL.DTO.ProductInOrder, App.Domain.ProductInOrder>().ReverseMap();
    }
}