using AutoMapper;
using Base.DAL;

namespace App.DAL.EF.Mappers;

public class SpecificationMapper : BaseMapper<App.DAL.DTO.Specification, App.Domain.Specification>
{
    public SpecificationMapper(IMapper mapper) : base(mapper)
    {
    }
}