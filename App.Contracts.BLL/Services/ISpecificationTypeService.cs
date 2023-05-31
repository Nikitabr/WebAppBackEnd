using Base.Contracts.BLL;

namespace App.Contracts.BLL.Services;

public interface ISpecificationTypeService : IEntityService<App.BLL.DTO.SpecificationType>, ISpecificationTypeRepositoryCustom<App.BLL.DTO.SpecificationType>
{

}

public interface ISpecificationTypeRepositoryCustom<TEntity>
{

}