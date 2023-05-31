using AutoMapper;
using Base.DAL;

namespace App.BLL.Mappers;

public class ProductInPcMapper : BaseMapper<App.BLL.DTO.ProductInPc, App.DAL.DTO.ProductInPc>
{
public ProductInPcMapper(IMapper mapper) : base(mapper)
{
}
}