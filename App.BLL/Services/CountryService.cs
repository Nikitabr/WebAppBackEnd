using App.BLL.DTO;
using App.Contracts.BLL.Services;
using App.Contracts.DAL;
using Base.BLL;
using Base.Contracts;
using Base.Contracts.BLL;

namespace App.BLL.Services;

public class CountryService : BaseEntityService<App.BLL.DTO.Country, App.DAL.DTO.Country, ICountryRepository>,
    ICountryService
{
    public CountryService(ICountryRepository repository, IMapper<Country, DAL.DTO.Country> mapper) : base(repository,
        mapper)
    {
    }


    public new async Task<IEnumerable<Country>> GetAllAsync(bool noTracking = true)
    {
        var res = (await Repository.GetAllAsync( noTracking)).Select(x => Mapper.Map(x)!).ToList();
        
        return res; 
    }

}