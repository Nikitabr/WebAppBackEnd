using App.BLL.DTO;
using App.BLL.DTO.Identity;
using AutoMapper;

namespace App.BLL;

public class AutomapperConfig : Profile
{
    public AutomapperConfig()
    {
        CreateMap<App.BLL.DTO.Country, App.DAL.DTO.Country>().ReverseMap();
        CreateMap<App.BLL.DTO.Feedback, App.DAL.DTO.Feedback>().ReverseMap();
        CreateMap<App.BLL.DTO.Invoice, App.DAL.DTO.Invoice>().ReverseMap();
        CreateMap<App.BLL.DTO.Order, App.DAL.DTO.Order>().ReverseMap();
        CreateMap<App.BLL.DTO.PaymentType, App.DAL.DTO.PaymentType>().ReverseMap();
        CreateMap<App.BLL.DTO.Pc, App.DAL.DTO.Pc>().ReverseMap();
        CreateMap<App.BLL.DTO.Product, App.DAL.DTO.Product>().ReverseMap();
        CreateMap<App.BLL.DTO.ProductInOrder, App.DAL.DTO.ProductInOrder>().ReverseMap();
        CreateMap<App.BLL.DTO.ProductInPc, App.DAL.DTO.ProductInPc>().ReverseMap();
        CreateMap<App.BLL.DTO.ProductType, App.DAL.DTO.ProductType>().ReverseMap();
        CreateMap<App.BLL.DTO.Service, App.DAL.DTO.Service>().ReverseMap();
        CreateMap<App.BLL.DTO.ServiceType, App.DAL.DTO.ServiceType>().ReverseMap();
        CreateMap<App.BLL.DTO.ShippingInfo, App.DAL.DTO.ShippingInfo>().ReverseMap();
        CreateMap<App.BLL.DTO.ShippingType, App.DAL.DTO.ShippingType>().ReverseMap();
        CreateMap<App.BLL.DTO.Specification, App.DAL.DTO.Specification>().ReverseMap();
        CreateMap<App.BLL.DTO.SpecificationType, App.DAL.DTO.SpecificationType>().ReverseMap();
        CreateMap<App.BLL.DTO.Identity.AppUser, App.DAL.DTO.Identity.AppUser>().ReverseMap();
        CreateMap<App.BLL.DTO.Identity.AppRole, App.DAL.DTO.Identity.AppRole>().ReverseMap();
        CreateMap<App.BLL.DTO.Identity.RefreshToken, App.DAL.DTO.Identity.RefreshToken>().ReverseMap();
    }
}