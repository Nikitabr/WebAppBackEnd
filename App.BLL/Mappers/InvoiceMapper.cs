using AutoMapper;
using Base.DAL;

namespace App.BLL.Mappers;

public class InvoiceMapper : BaseMapper<App.BLL.DTO.Invoice, App.DAL.DTO.Invoice>
{
public InvoiceMapper(IMapper mapper) : base(mapper)
{
}
}