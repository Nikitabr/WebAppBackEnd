using App.DAL.DTO;
using Base.Contracts.DAL;

namespace App.Contracts.DAL;

public interface IServiceRepository : IEntityRepository<App.DAL.DTO.Service>, IServiceRepositoryCustom<Service>
{
    
}
public interface IServiceRepositoryCustom<TEntity>
{
    // write your custom methods here 
    
    
}