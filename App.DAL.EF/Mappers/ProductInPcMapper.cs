using AutoMapper;
using Base.DAL;

namespace App.DAL.EF.Mappers;

public class ProductInPcMapper : BaseMapper<App.DAL.DTO.ProductInPc, App.Domain.ProductInPc>
{
    public ProductInPcMapper(IMapper mapper) : base(mapper)
    {
    }
}