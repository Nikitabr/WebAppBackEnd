using AutoMapper;
using Base.DAL;

namespace App.BLL.Mappers;

public class SpecificationMapper : BaseMapper<App.BLL.DTO.Specification, App.DAL.DTO.Specification>
{
    public SpecificationMapper(IMapper mapper) : base(mapper)
    {
    }
}