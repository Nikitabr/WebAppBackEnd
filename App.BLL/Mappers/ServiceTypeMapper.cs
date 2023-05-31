using AutoMapper;
using Base.DAL;

namespace App.BLL.Mappers;

public class ServiceTypeMapper : BaseMapper<App.BLL.DTO.ServiceType, App.DAL.DTO.ServiceType>
{
    public ServiceTypeMapper(IMapper mapper) : base(mapper)
    {
    }
}