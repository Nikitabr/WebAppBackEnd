using AutoMapper;
using Base.DAL;

namespace App.DAL.EF.Mappers;

public class ProductInOrderMapper : BaseMapper<App.DAL.DTO.ProductInOrder, App.Domain.ProductInOrder>
{
    public ProductInOrderMapper(IMapper mapper) : base(mapper)
    {
    }
}