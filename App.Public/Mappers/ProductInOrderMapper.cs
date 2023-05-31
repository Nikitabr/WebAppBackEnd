using AutoMapper;
using Base.DAL;

namespace App.Public.Mappers;

public class ProductInOrderMapper : BaseMapper<App.Public.DTO.v1.ProductInOrder, App.BLL.DTO.ProductInOrder>
{
    public ProductInOrderMapper(IMapper mapper) : base(mapper)
    {
    }
    
    public static App.Public.DTO.v1.ProductInOrder ToPublic(App.BLL.DTO.ProductInOrder productInOrder)
    {
        return new DTO.v1.ProductInOrder
        {
            Id = productInOrder.Id,
            ProductId = productInOrder.ProductId,
            OrderId = productInOrder.OrderId,

        };
    }
    
    public static App.BLL.DTO.ProductInOrder ToBll(App.Public.DTO.v1.ProductInOrder productInOrder)
    {
        return new App.BLL.DTO.ProductInOrder
        {
            Id = productInOrder.Id,
            ProductId = productInOrder.ProductId,
            OrderId = productInOrder.OrderId,

        };
    }
}