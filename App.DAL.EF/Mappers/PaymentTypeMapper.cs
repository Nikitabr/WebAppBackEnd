using AutoMapper;
using Base.DAL;

namespace App.DAL.EF.Mappers;

public class PaymentTypeMapper : BaseMapper<App.DAL.DTO.PaymentType, App.Domain.PaymentType>
{
    public PaymentTypeMapper(IMapper mapper) : base(mapper)
    {
    }
}