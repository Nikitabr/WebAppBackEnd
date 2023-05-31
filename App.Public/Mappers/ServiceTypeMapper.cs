using AutoMapper;
using Base.DAL;

namespace App.Public.Mappers;

public class ServiceTypeMapper : BaseMapper<App.Public.DTO.v1.ServiceType, App.BLL.DTO.ServiceType>
{
    public ServiceTypeMapper(IMapper mapper) : base(mapper)
    {
    }
    
    public static App.Public.DTO.v1.ServiceType ToPublic(App.BLL.DTO.ServiceType serviceType)
    {
        return new DTO.v1.ServiceType()
        {
            Id = serviceType.Id,
            Title = serviceType.Title,
            Services = serviceType.Services != null ?
                serviceType.Services.Select(ServiceMapper.ToPublic).ToList() :
                new List<App.Public.DTO.v1.Service>()
        };
    }
    
    public static App.BLL.DTO.ServiceType ToBll(App.Public.DTO.v1.ServiceType serviceType)
    {
        return new App.BLL.DTO.ServiceType()
        {
            Id = serviceType.Id,
            Title = serviceType.Title,
            Services = serviceType.Services != null ?
                serviceType.Services.Select(ServiceMapper.ToBll).ToList() :
                new List<App.BLL.DTO.Service>()
        };
    }
}