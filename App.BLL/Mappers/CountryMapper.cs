using AutoMapper;
using Base.DAL;

namespace App.BLL.Mappers;

public class CountryMapper : BaseMapper<App.BLL.DTO.Country, App.DAL.DTO.Country>
{
    public CountryMapper(IMapper mapper) : base(mapper)
    {
    }
}