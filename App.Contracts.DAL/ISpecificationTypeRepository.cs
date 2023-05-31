using App.DAL.DTO;
using Base.Contracts.DAL;

namespace App.Contracts.DAL;

public interface ISpecificationTypeRepository : IEntityRepository<App.DAL.DTO.SpecificationType>, ISpecificationTypeRepositoryCustom<SpecificationType>
{
    
}

public interface ISpecificationTypeRepositoryCustom<TEntity>
{
    // write your custom methods here 
    
    
}