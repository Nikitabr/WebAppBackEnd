using App.Public.DTO.v1;
using AutoMapper;
using Base.DAL;

namespace App.Public.Mappers;

public class PaymentTypeMapper : BaseMapper<App.Public.DTO.v1.PaymentType, App.BLL.DTO.PaymentType>
{
    public PaymentTypeMapper(IMapper mapper) : base(mapper)
    {
    }

    public static App.Public.DTO.v1.PaymentType ToPublic(App.BLL.DTO.PaymentType paymentType)
    {
        return new DTO.v1.PaymentType()
        {
            Id = paymentType.Id,
            PaymentName = paymentType.PaymentName,
            Description = paymentType.Description,
            Orders = paymentType.Orders != null ?
                paymentType.Orders.Select(OrderMapper.ToPublic).ToList() :
                new List<Order>(),
        };
    }
    
    public static App.BLL.DTO.PaymentType ToBll(App.Public.DTO.v1.PaymentType paymentType)
    {
        return new App.BLL.DTO.PaymentType()
        {
            Id = paymentType.Id,
            PaymentName = paymentType.PaymentName,
            Description = paymentType.Description,
            Orders = paymentType.Orders != null ?
                paymentType.Orders.Select(OrderMapper.ToBll).ToList() :
                new List<BLL.DTO.Order>()
        };
    }
    
}