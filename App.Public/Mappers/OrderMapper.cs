using App.Public.DTO.v1;
using AutoMapper;
using Base.DAL;

namespace App.Public.Mappers;

public class OrderMapper : BaseMapper<App.Public.DTO.v1.Order, App.BLL.DTO.Order>
{
    public OrderMapper(IMapper mapper) : base(mapper)
    {
    }
    
    public static App.Public.DTO.v1.Order ToPublic(App.BLL.DTO.Order order)
    {
        return new App.Public.DTO.v1.Order()
        {
            AppUserId = order.AppUserId,
            DateTime = order.DateTime,
            Description = order.Description,
            ShippingInfoId = order.ShippingInfoId,
            PaymentTypeId = order.PaymentTypeId,
            Id = order.Id,
            Price = order.Price,
            Invoices = order.Invoices != null ?
                order.Invoices.Select(InvoiceMapper.ToPublic).ToList() :
                new List<App.Public.DTO.v1.Invoice>(),
            ProductInOrders = order.ProductInOrders != null ?
                order.ProductInOrders.Select(ProductInOrderMapper.ToPublic).ToList() :
                new List<ProductInOrder>()
        };
    }
    
    public static App.BLL.DTO.Order ToBll(App.Public.DTO.v1.Order order)
    {
        return new App.BLL.DTO.Order()
        {
            AppUserId = order.AppUserId,
            DateTime = DateTime.UtcNow,
            Description = order.Description,
            ShippingInfoId = order.ShippingInfoId,
            PaymentTypeId = order.PaymentTypeId,
            Id = order.Id,
            Price = order.Price,
            Invoices = order.Invoices != null ?
                order.Invoices.Select(InvoiceMapper.ToBll).ToList() :
                new List<App.BLL.DTO.Invoice>(),
            ProductInOrders = order.ProductInOrders != null ?
                order.ProductInOrders.Select(ProductInOrderMapper.ToBll).ToList() :
                new List<BLL.DTO.ProductInOrder>()
        };
    }
}