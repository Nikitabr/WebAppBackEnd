using AutoMapper;
using Base.Contracts;
using Base.DAL;

namespace App.DAL.EF.Mappers;

public class PcMapper : BaseMapper<App.DAL.DTO.Pc, App.Domain.Pc>
{
    public PcMapper(IMapper mapper) : base(mapper)
    {
    }
}