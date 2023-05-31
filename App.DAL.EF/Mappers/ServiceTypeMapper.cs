using AutoMapper;
using Base.DAL;

namespace App.DAL.EF.Mappers;

public class ServiceTypeMapper : BaseMapper<App.DAL.DTO.ServiceType, App.Domain.ServiceType>
{
    public ServiceTypeMapper(IMapper mapper) : base(mapper)
    {
    }
}