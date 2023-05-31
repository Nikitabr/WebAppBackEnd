using AutoMapper;
using Base.DAL;


namespace App.Public.Mappers;

public class CountryMapper : BaseMapper<App.Public.DTO.v1.Country, App.BLL.DTO.Country>
{
    public CountryMapper(IMapper mapper) : base(mapper)
    {
    }

    public static App.Public.DTO.v1.Country ToPublic(App.BLL.DTO.Country country)
    {
        return new DTO.v1.Country()
        {
           ShippingInfos = country.ShippingInfos != null ?
               country.ShippingInfos.Select(ShippingInfoMapper.ToPublic).ToList() :
               new List<App.Public.DTO.v1.ShippingInfo>(),
           Id = country.Id,
           CountryName = country.CountryName
        };
    }
    
    public static App.BLL.DTO.Country ToBll(App.Public.DTO.v1.Country country)
    {
        return new App.BLL.DTO.Country()
        {
            
            Id = country.Id, 
            CountryName = country.CountryName, 
            ShippingInfos = country.ShippingInfos != null ?
               country.ShippingInfos.Select(ShippingInfoMapper.ToBll).ToList() :
               new List<App.BLL.DTO.ShippingInfo>()
        };
    }
    
}