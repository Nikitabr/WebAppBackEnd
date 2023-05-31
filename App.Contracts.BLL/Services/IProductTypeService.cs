using Base.Contracts.BLL;

namespace App.Contracts.BLL.Services;

public interface IProductTypeService : IEntityService<App.BLL.DTO.ProductType>, IProductTypeRepositoryCustom<App.BLL.DTO.ProductType>
{

}

public interface IProductTypeRepositoryCustom<TEntity>
{

}