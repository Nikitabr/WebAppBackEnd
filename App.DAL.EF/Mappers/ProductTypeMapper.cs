using AutoMapper;
using Base.Contracts;
using Base.DAL;

namespace App.DAL.EF.Mappers;

public class ProductTypeMapper : BaseMapper<App.DAL.DTO.ProductType, App.Domain.ProductType>
{
    public ProductTypeMapper(IMapper mapper) : base(mapper)
    {
    }
}