using App.DAL.DTO;
using Base.Contracts.DAL;

namespace App.Contracts.DAL;

public interface IOrderRepository : IEntityRepository<App.DAL.DTO.Order>, IOrderRepositoryCustom<Order>
{

}

public interface IOrderRepositoryCustom<TEntity>
{
    // write your custom methods here 
    Task<IEnumerable<TEntity>> GetAllOrdersByUserId(string userId,  bool noTracking = true);
    

}