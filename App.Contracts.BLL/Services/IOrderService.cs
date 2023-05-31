using App.BLL.DTO;
using App.Contracts.DAL;
using Base.Contracts.BLL;

namespace App.Contracts.BLL.Services;

public interface IOrderService : IEntityService<App.BLL.DTO.Order>, IOrderRepositoryCustom<App.BLL.DTO.Order>
{
    
}

public interface IOrderRepositoryCustom<TEntity>
{
    Task<IEnumerable<TEntity>> GetAllOrdersByUserId(string userId, bool noTracking = true);

    TEntity Add(Order order, List<String> products, IEnumerable<Product> productsList);
}