using AutoMapper;
using Base.DAL;

namespace App.DAL.EF.Mappers;

public class SpecificationTypeMapper : BaseMapper<App.DAL.DTO.SpecificationType, App.Domain.SpecificationType>
{
    public SpecificationTypeMapper(IMapper mapper) : base(mapper)
    {
    }
}