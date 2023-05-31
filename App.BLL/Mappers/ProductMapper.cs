using AutoMapper;
using Base.Contracts;
using Base.DAL;

namespace App.BLL.Mappers;

public class ProductMapper : BaseMapper<App.BLL.DTO.Product, App.DAL.DTO.Product>
{
    public ProductMapper(IMapper mapper) : base(mapper)
    {
    }
}