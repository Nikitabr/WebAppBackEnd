using App.Contracts.DAL;
using App.DAL.DTO;
using Base.Contracts;
using Base.Contracts.BLL;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class CountryRepository : BaseEntityRepository<App.DAL.DTO.Country, App.Domain.Country, AppDbContext>, ICountryRepository
{
    public CountryRepository(AppDbContext dbContext, IMapper<Country, Domain.Country> mapper) : base(dbContext, mapper)
    {
    }
    
    public override async Task<IEnumerable<Country>> GetAllAsync(bool noTracking = true)
    {
        var query = CreateQuery(noTracking);
        return (await query.ToListAsync()).Select(x => Mapper.Map(x)!);    
    }
}