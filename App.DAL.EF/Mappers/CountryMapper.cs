using AutoMapper;
using Base.DAL;

namespace App.DAL.EF.Mappers;

public class CountryMapper : BaseMapper<App.DAL.DTO.Country, App.Domain.Country>
{
    public CountryMapper(IMapper mapper) : base(mapper)
    {
    }
}