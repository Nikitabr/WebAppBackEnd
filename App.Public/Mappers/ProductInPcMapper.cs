using AutoMapper;
using Base.DAL;

namespace App.Public.Mappers;

public class ProductInPcMapper : BaseMapper<App.Public.DTO.v1.ProductInPc, App.BLL.DTO.ProductInPc>
{
    public ProductInPcMapper(IMapper mapper) : base(mapper)
    {
    }
    
    public static App.Public.DTO.v1.ProductInPc ToPublic(App.BLL.DTO.ProductInPc productInPc)
    {
        return new DTO.v1.ProductInPc()
        {
            Id = productInPc.Id,
            PcId = productInPc.PcId,
            ProductId = productInPc.ProductId
        };
    }
    
    public static App.BLL.DTO.ProductInPc ToBll(App.Public.DTO.v1.ProductInPc productInPc)
    {
        return new App.BLL.DTO.ProductInPc()
        {
            Id = productInPc.Id,
            PcId = productInPc.PcId,
            ProductId = productInPc.ProductId
        };
    }
    
}