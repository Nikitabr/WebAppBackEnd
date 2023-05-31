using AutoMapper;
using Base.DAL;

namespace App.DAL.EF.Mappers;

public class InvoiceMapper : BaseMapper<App.DAL.DTO.Invoice, App.Domain.Invoice>
{
    public InvoiceMapper(IMapper mapper) : base(mapper)
    {
    }
}