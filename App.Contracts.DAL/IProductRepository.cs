using App.DAL.DTO;
using Base.Contracts.DAL;

namespace App.Contracts.DAL;

public interface IProductRepository : IEntityRepository<App.DAL.DTO.Product>, IProductRepositoryCustom<Product>
{
    
}
public interface IProductRepositoryCustom<TEntity>
{
    // write your custom methods here 

    Task<IEnumerable<TEntity>> GetProductByName(string productName);

}