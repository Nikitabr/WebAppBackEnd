
using AutoMapper;
using Base.DAL;

namespace App.Public.Mappers;

public class ShippingTypeMapper : BaseMapper<App.Public.DTO.v1.ShippingType, App.BLL.DTO.ShippingType>
{
    public ShippingTypeMapper(IMapper mapper) : base(mapper)
    {
    }
    
    public static App.Public.DTO.v1.ShippingType ToPublic(App.BLL.DTO.ShippingType shippingType)
    {
        return new DTO.v1.ShippingType()
        {
            Id = shippingType.Id,
            Title = shippingType.Title,
            ShippingInfos = shippingType.ShippingInfos != null ?
                shippingType.ShippingInfos.Select(ShippingInfoMapper.ToPublic).ToList() :
                new List<App.Public.DTO.v1.ShippingInfo>()
        };
    }
    
    public static App.BLL.DTO.ShippingType ToBll(App.Public.DTO.v1.ShippingType shippingType)
    {
        return new App.BLL.DTO.ShippingType()
        {
            Id = shippingType.Id,
            Title = shippingType.Title,
            ShippingInfos = shippingType.ShippingInfos != null ?
                shippingType.ShippingInfos.Select(ShippingInfoMapper.ToBll).ToList() :
                new List<App.BLL.DTO.ShippingInfo>()
        };
    }
}