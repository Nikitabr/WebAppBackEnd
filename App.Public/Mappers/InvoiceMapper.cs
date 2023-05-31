using AutoMapper;
using Base.DAL;

namespace App.Public.Mappers;

public class InvoiceMapper : BaseMapper<App.Public.DTO.v1.Invoice, App.BLL.DTO.Invoice>
{
    public InvoiceMapper(IMapper mapper) : base(mapper)
    {
    }
    
    public static App.Public.DTO.v1.Invoice ToPublic(App.BLL.DTO.Invoice invoice)
    {
        return new App.Public.DTO.v1.Invoice()
        {
            FirstName = invoice.FirstName,
            LastName = invoice.LastName,
            DeliveryMethod = invoice.DeliveryMethod,
            PaymentMethod = invoice.PaymentMethod,
            Email = invoice.Email,
            Code = invoice.Code,
            Id = invoice.Id,
            OrderId = invoice.OrderId
        };
    }
    
    public static App.BLL.DTO.Invoice ToBll(App.Public.DTO.v1.Invoice invoice)
    {
        return new App.BLL.DTO.Invoice()
        {
            FirstName = invoice.FirstName,
            LastName = invoice.LastName,
            DeliveryMethod = invoice.DeliveryMethod,
            PaymentMethod = invoice.PaymentMethod,
            Email = invoice.Email,
            Code = invoice.Code,
            Id = invoice.Id,
            OrderId = invoice.OrderId
        };
    }
}