using AutoMapper;
using Base.DAL;

namespace App.Public.Mappers;

public class ServiceMapper : BaseMapper<App.Public.DTO.v1.Service, App.BLL.DTO.Service>
{
    public ServiceMapper(IMapper mapper) : base(mapper)
    {
    }
    
    public static App.Public.DTO.v1.Service ToPublic(App.BLL.DTO.Service service)
    {
        return new DTO.v1.Service()
        {
            Id = service.Id,
            AppUserId = service.AppUserId,
            Description = service.Description,
            ServiceTypeId = service.ServiceTypeId,
            Orders = service.Orders != null ?
                service.Orders.Select(OrderMapper.ToPublic).ToList() :
                new List<App.Public.DTO.v1.Order>()
        };
    }
    
    public static App.BLL.DTO.Service ToBll(App.Public.DTO.v1.Service service)
    {
        return new App.BLL.DTO.Service()
        {
            Id = service.Id,
            AppUserId = service.AppUserId,
            Description = service.Description,
            ServiceTypeId = service.ServiceTypeId,
            Orders = service.Orders != null ?
                service.Orders.Select(OrderMapper.ToBll).ToList() :
                new List<App.BLL.DTO.Order>()
        };
    }
}