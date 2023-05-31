using App.BLL.DTO;
using AutoMapper;
using Base.DAL;

namespace App.Public.Mappers;

public class ProductMapper : BaseMapper<App.Public.DTO.v1.Product, App.BLL.DTO.Product>
{
    public ProductMapper(IMapper mapper) : base(mapper)
    {
    }
    
    public static App.Public.DTO.v1.Product ToPublic(App.BLL.DTO.Product product)
    {
        return new DTO.v1.Product()
        {
            Id = product.Id,
            Description = product.Description,
            Price = product.Price,
            Title = product.Title,
            Rating = product.Rating,
            Quantity = product.Quantity,
            Picture = product.Picture,
            ProductTypeId = product.ProductTypeId,
            Feedbacks = product.Feedbacks != null ?
                product.Feedbacks.Select(FeedbackMapper.ToPublic).ToList() :
                new List<App.Public.DTO.v1.Feedback>(),
            ProductInPcs = product.ProductInPcs != null ?
                product.ProductInPcs.Select(ProductInPcMapper.ToPublic).ToList() :
                new List<App.Public.DTO.v1.ProductInPc>(),
            SpecificationTypes = product.SpecificationTypes != null ?
                product.SpecificationTypes.Select(SpecificationTypeMapper.ToPublic).ToList() :
                new List<App.Public.DTO.v1.SpecificationType>(),
            ProductInOrders = product.ProductInOrders != null ?
                product.ProductInOrders.Select(ProductInOrderMapper.ToPublic).ToList() :
                new List<App.Public.DTO.v1.ProductInOrder>()
            
        };
    }
    
    public static App.BLL.DTO.Product ToBll(App.Public.DTO.v1.Product product)
    {
        return new App.BLL.DTO.Product()
        {
            Id = product.Id,
            Description = product.Description,
            Price = product.Price,
            Title = product.Title,
            Rating = product.Rating,
            Quantity = product.Quantity,
            Picture = product.Picture,
            ProductTypeId = product.ProductTypeId,
            Feedbacks = product.Feedbacks != null ?
                product.Feedbacks.Select(FeedbackMapper.ToBll).ToList() :
                new List<App.BLL.DTO.Feedback>(),
            ProductInPcs = product.ProductInPcs != null ?
                product.ProductInPcs.Select(ProductInPcMapper.ToBll).ToList() :
                new List<App.BLL.DTO.ProductInPc>(),
            SpecificationTypes = product.SpecificationTypes != null ?
                product.SpecificationTypes.Select(SpecificationTypeMapper.ToBll).ToList() :
                new List<App.BLL.DTO.SpecificationType>(),
            ProductInOrders = product.ProductInOrders != null ?
                product.ProductInOrders.Select(ProductInOrderMapper.ToBll).ToList() :
                new List<ProductInOrder>()
        };
    }
}