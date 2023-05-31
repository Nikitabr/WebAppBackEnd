using App.DAL.DTO;
using Base.Contracts.DAL;

namespace App.Contracts.DAL;

public interface ICountryRepository : IEntityRepository<App.DAL.DTO.Country>, ICountryRepositoryCustom<Country>
{
    
}

public interface ICountryRepositoryCustom<TEntity>
{
    // write your custom methods here 

}