using AutoMapper;
using Base.DAL;

namespace App.Public.Mappers;

public class SpecificationMapper : BaseMapper<App.Public.DTO.v1.Specification, App.BLL.DTO.Specification>
{
    public SpecificationMapper(IMapper mapper) : base(mapper)
    {
    }
    
    public static App.Public.DTO.v1.Specification ToPublic(App.BLL.DTO.Specification specification)
    {
        return new DTO.v1.Specification()
        {
            Id = specification.Id,
            SpecificationName = specification.SpecificationName,
            Description = specification.Description,
            SpecificationTypeId = specification.SpecificationTypeId
        };
    }
    
    public static App.BLL.DTO.Specification ToBll(App.Public.DTO.v1.Specification specification)
    {
        return new App.BLL.DTO.Specification()
        {
            Id = specification.Id,
            SpecificationName = specification.SpecificationName,
            Description = specification.Description,
            SpecificationTypeId = specification.SpecificationTypeId
        };
    }
}