using Base.Contracts.BLL;

namespace App.Contracts.BLL.Services;

public interface IServiceService : IEntityService<App.BLL.DTO.Service>, IServiceRepositoryCustom<App.BLL.DTO.Service>
{

}

public interface IServiceRepositoryCustom<TEntity>
{

}