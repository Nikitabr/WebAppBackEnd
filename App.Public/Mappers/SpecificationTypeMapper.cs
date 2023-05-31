using AutoMapper;
using Base.DAL;

namespace App.Public.Mappers;

public class SpecificationTypeMapper : BaseMapper<App.Public.DTO.v1.SpecificationType, App.BLL.DTO.SpecificationType>
{
    public SpecificationTypeMapper(IMapper mapper) : base(mapper)
    {
    }
    
    public static App.Public.DTO.v1.SpecificationType ToPublic(App.BLL.DTO.SpecificationType specificationType)
    {
        return new DTO.v1.SpecificationType()
        {
            Id = specificationType.Id,
            Title = specificationType.Title,
            ProductId = specificationType.ProductId,
            Specifications = specificationType.Specifications != null ?
                specificationType.Specifications.Select(SpecificationMapper.ToPublic).ToList() :
                new List<App.Public.DTO.v1.Specification>()
        };
    }
    
    public static App.BLL.DTO.SpecificationType ToBll(App.Public.DTO.v1.SpecificationType specificationType)
    {
        return new App.BLL.DTO.SpecificationType()
        {
            Id = specificationType.Id,
            Title = specificationType.Title,
            ProductId = specificationType.ProductId,
            Specifications = specificationType.Specifications != null ?
                specificationType.Specifications.Select(SpecificationMapper.ToBll).ToList() :
                new List<App.BLL.DTO.Specification>()
        };
    }
}