using AutoMapper;
using Base.Contracts;
using Base.DAL;

namespace App.DAL.EF.Mappers;

public class ProductMapper : BaseMapper<App.DAL.DTO.Product, App.Domain.Product>
{
    public ProductMapper(IMapper mapper) : base(mapper)
    {
    }
}