using Base.Contracts.BLL;

namespace App.Contracts.BLL.Services;

public interface IProductInPcService : IEntityService<App.BLL.DTO.ProductInPc>, IProductInPcRepositoryCustom<App.BLL.DTO.ProductInPc>
{

}

public interface IProductInPcRepositoryCustom<TEntity>
{

}