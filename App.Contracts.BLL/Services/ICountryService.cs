using App.Contracts.DAL;
using Base.Contracts.BLL;

namespace App.Contracts.BLL.Services;

public interface ICountryService : IEntityService<App.BLL.DTO.Country>, ICountryRepositoryCustom<App.BLL.DTO.Country>
{
    
}

public interface ICountryRepositoryCustom<TEntity>
{

}