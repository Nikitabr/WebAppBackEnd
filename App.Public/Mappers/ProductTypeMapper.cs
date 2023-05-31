using AutoMapper;
using Base.Contracts;
using Base.DAL;

namespace App.Public.Mappers;

public class ProductTypeMapper : BaseMapper<App.Public.DTO.v1.ProductType, App.BLL.DTO.ProductType>
{
    public ProductTypeMapper(IMapper mapper) : base(mapper)
    {
    }
    
    public static App.Public.DTO.v1.ProductType ToPublic(App.BLL.DTO.ProductType productType)
    {
        return new DTO.v1.ProductType()
        {
            Id = productType.Id,
            Title = productType.Title,
            Products = productType.Products != null ?
                productType.Products.Select(ProductMapper.ToPublic).ToList() :
                new List<App.Public.DTO.v1.Product>()
        };
    }
    
    public static App.BLL.DTO.ProductType ToBll(App.Public.DTO.v1.ProductType productType)
    {
        return new App.BLL.DTO.ProductType()
        {
            Id = productType.Id,
            Title = productType.Title,
            Products = productType.Products != null ?
                productType.Products.Select(ProductMapper.ToBll).ToList() :
                new List<App.BLL.DTO.Product>()
        };
    }
}