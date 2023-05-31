using AutoMapper;
using Base.DAL;

namespace App.BLL.Mappers;

public class ProductInOrderMapper : BaseMapper<App.BLL.DTO.ProductInOrder, App.DAL.DTO.ProductInOrder>
{
    public ProductInOrderMapper(IMapper mapper) : base(mapper)
    {
    }
}