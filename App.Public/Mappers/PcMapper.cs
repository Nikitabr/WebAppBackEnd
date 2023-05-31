using AutoMapper;
using Base.DAL;

namespace App.Public.Mappers;

public class PcMapper : BaseMapper<App.Public.DTO.v1.Pc, App.BLL.DTO.Pc>
{
    public PcMapper(IMapper mapper) : base(mapper)
    {
    }
    
    public static App.Public.DTO.v1.Pc ToPublic(App.BLL.DTO.Pc pc)
    {
        return new DTO.v1.Pc()
        {
            Description = pc.Description,
            Id = pc.Id,
            Price = pc.Price,
            Title = pc.Title,
            Orders = pc.Orders != null ?
                pc.Orders.Select(OrderMapper.ToPublic).ToList() :
                new List<App.Public.DTO.v1.Order>(),
            Feedbacks = pc.Feedbacks != null ?
                pc.Feedbacks.Select(FeedbackMapper.ToPublic).ToList() :
                new List<App.Public.DTO.v1.Feedback>(),
            ProductInPcs = pc.ProductInPcs != null ?
                pc.ProductInPcs.Select(ProductInPcMapper.ToPublic).ToList() :
                new List<App.Public.DTO.v1.ProductInPc>()
        };
    }
    
    public static App.BLL.DTO.Pc ToBll(App.Public.DTO.v1.Pc pc)
    {
        return new App.BLL.DTO.Pc()
        {
            Description = pc.Description,
            Id = pc.Id,
            Price = pc.Price,
            Title = pc.Title,
            Orders = pc.Orders != null ?
                pc.Orders.Select(OrderMapper.ToBll).ToList() :
                new List<App.BLL.DTO.Order>(),
            Feedbacks = pc.Feedbacks != null ?
                pc.Feedbacks.Select(FeedbackMapper.ToBll).ToList() :
                new List<App.BLL.DTO.Feedback>(),
            ProductInPcs = pc.ProductInPcs != null ?
                pc.ProductInPcs.Select(ProductInPcMapper.ToBll).ToList() :
                new List<App.BLL.DTO.ProductInPc>()
        };
    }
}