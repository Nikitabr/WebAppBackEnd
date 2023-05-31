using App.DAL.DTO;
using Base.Contracts.DAL;

namespace App.Contracts.DAL;

public interface IServiceTypeRepository : IEntityRepository<App.DAL.DTO.ServiceType>, IServiceTypeRepositoryCustom<ServiceType>
{
    
}

public interface IServiceTypeRepositoryCustom<TEntity>
{
    // write your custom methods here 
    
}