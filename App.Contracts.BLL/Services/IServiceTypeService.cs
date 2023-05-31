using Base.Contracts.BLL;

namespace App.Contracts.BLL.Services;

public interface IServiceTypeService : IEntityService<App.BLL.DTO.ServiceType>, IServiceTypeRepositoryCustom<App.BLL.DTO.ServiceType>
{

}

public interface IServiceTypeRepositoryCustom<TEntity>
{

}