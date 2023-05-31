using AutoMapper;
using Base.DAL;

namespace App.DAL.EF.Mappers;

public class ShippingTypeMapper : BaseMapper<App.DAL.DTO.ShippingType, App.Domain.ShippingType>
{
    public ShippingTypeMapper(IMapper mapper) : base(mapper)
    {
    }
}