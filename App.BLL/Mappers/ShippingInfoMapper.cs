using AutoMapper;
using Base.DAL;

namespace App.BLL.Mappers;

public class ShippingInfoMapper : BaseMapper<App.BLL.DTO.ShippingInfo, App.DAL.DTO.ShippingInfo>
{
    public ShippingInfoMapper(IMapper mapper) : base(mapper)
    {
    }
}