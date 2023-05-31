using App.DAL.DTO;
using Base.Contracts.DAL;

namespace App.Contracts.DAL;

public interface IProductInPcRepository : IEntityRepository<App.DAL.DTO.ProductInPc>, IProductInPcRepositoryCustom<ProductInPc>
{
    
}
public interface IProductInPcRepositoryCustom<TEntity>
{
    // write your custom methods here 
    
}