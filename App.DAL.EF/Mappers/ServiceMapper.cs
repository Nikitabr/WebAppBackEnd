using AutoMapper;
using Base.DAL;

namespace App.DAL.EF.Mappers;

public class ServiceMapper : BaseMapper<App.DAL.DTO.Service, App.Domain.Service>
{
    public ServiceMapper(IMapper mapper) : base(mapper)
    {
    }
}