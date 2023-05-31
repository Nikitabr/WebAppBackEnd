using App.DAL.DTO;
using Base.Contracts.DAL;

namespace App.Contracts.DAL;

public interface IProductTypeRepository : IEntityRepository<App.DAL.DTO.ProductType>, IProductTypeRepositoryCustom<ProductType>
{
    // write your custom methods here
    
}

public interface IProductTypeRepositoryCustom<TEntity>
{
    // write your custom methods here 
    
    
}