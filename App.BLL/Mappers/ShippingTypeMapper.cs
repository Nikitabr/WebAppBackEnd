using AutoMapper;
using Base.DAL;

namespace App.BLL.Mappers;

public class ShippingTypeMapper : BaseMapper<App.BLL.DTO.ShippingType, App.DAL.DTO.ShippingType>
{
    public ShippingTypeMapper(IMapper mapper) : base(mapper)
    {
    }
}