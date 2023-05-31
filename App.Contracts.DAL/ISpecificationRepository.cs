using App.DAL.DTO;
using Base.Contracts.DAL;

namespace App.Contracts.DAL;

public interface ISpecificationRepository : IEntityRepository<App.DAL.DTO.Specification>, ISpecificationRepositoryCustom<Specification>
{
    
}

public interface ISpecificationRepositoryCustom<TEntity>
{
    // write your custom methods here 
    
}