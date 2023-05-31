using App.DAL.DTO;
using Base.Contracts.DAL;

namespace App.Contracts.DAL;

public interface IProductInOrderRepository : IEntityRepository<App.DAL.DTO.ProductInOrder>, IProductInOrderRepositoryCustom<ProductInOrder>
{
    // write your custom methods here 

}

public interface IProductInOrderRepositoryCustom<TEntity>
{
    // write your custom methods here 
}