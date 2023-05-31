using Base.Contracts.BLL;

namespace App.Contracts.BLL.Services;

public interface IProductInOrderService : IEntityService<App.BLL.DTO.ProductInOrder>, IProductInOrderRepositoryCustom<App.BLL.DTO.ProductInOrder>
{

}

public interface IProductInOrderRepositoryCustom<TEntity>
{

}