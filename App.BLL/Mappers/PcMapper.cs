using AutoMapper;
using Base.Contracts;
using Base.DAL;

namespace App.BLL.Mappers;

public class PcMapper : BaseMapper<App.BLL.DTO.Pc, App.DAL.DTO.Pc>
{
    public PcMapper(IMapper mapper) : base(mapper)
    {
    }
}