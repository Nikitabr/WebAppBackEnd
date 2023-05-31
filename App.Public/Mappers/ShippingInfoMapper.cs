using App.Public.DTO.v1;
using AutoMapper;
using Base.DAL;

namespace App.Public.Mappers;

public class ShippingInfoMapper : BaseMapper<App.Public.DTO.v1.ShippingInfo, App.BLL.DTO.ShippingInfo>
{
    public ShippingInfoMapper(IMapper mapper) : base(mapper)
    {
    }


    public static App.Public.DTO.v1.ShippingInfo ToPublic(App.BLL.DTO.ShippingInfo shippingInfo)
    {
        return new App.Public.DTO.v1.ShippingInfo()
        {
            Address1 = shippingInfo.Address1,
            Address2 = shippingInfo.Address2,
            City = shippingInfo.City,
            CountryId = shippingInfo.CountryId,
            Id = shippingInfo.Id,
            MailAddress = shippingInfo.MailAddress,
            PhoneNumber = shippingInfo.PhoneNumber,
            PostalCode = shippingInfo.PostalCode,
            ShippingTypeId = shippingInfo.ShippingTypeId,
            Orders = shippingInfo.Orders != null ?
                shippingInfo.Orders.Select(OrderMapper.ToPublic).ToList() :
                new List<Order>()
        };
    }
    
    public static App.BLL.DTO.ShippingInfo ToBll(App.Public.DTO.v1.ShippingInfo shippingInfo)
    {
        return new App.BLL.DTO.ShippingInfo()
        {
            Address1 = shippingInfo.Address1,
            Address2 = shippingInfo.Address2,
            City = shippingInfo.City,
            CountryId = shippingInfo.CountryId,
            Id = shippingInfo.Id,
            MailAddress = shippingInfo.MailAddress,
            PhoneNumber = shippingInfo.PhoneNumber,
            PostalCode = shippingInfo.PostalCode,
            ShippingTypeId = shippingInfo.ShippingTypeId,
            Orders = shippingInfo.Orders != null ?
                shippingInfo.Orders.Select(OrderMapper.ToBll).ToList() :
                new List<BLL.DTO.Order>()
        };
    }
}