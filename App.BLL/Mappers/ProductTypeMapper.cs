using AutoMapper;
using Base.Contracts;
using Base.DAL;

namespace App.BLL.Mappers;

public class ProductTypeMapper : BaseMapper<App.BLL.DTO.ProductType, App.DAL.DTO.ProductType>
{
    public ProductTypeMapper(IMapper mapper) : base(mapper)
    {
    }
}