using AutoMapper;
using Base.DAL;

namespace App.BLL.Mappers;

public class SpecificationTypeMapper : BaseMapper<App.BLL.DTO.SpecificationType, App.DAL.DTO.SpecificationType>
{
    public SpecificationTypeMapper(IMapper mapper) : base(mapper)
    {
    }
}