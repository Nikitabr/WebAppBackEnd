using Base.Contracts.BLL;

namespace App.Contracts.BLL.Services;

public interface ISpecificationService : IEntityService<App.BLL.DTO.Specification>, ISpecificationRepositoryCustom<App.BLL.DTO.Specification>
{

}

public interface ISpecificationRepositoryCustom<TEntity>
{

}