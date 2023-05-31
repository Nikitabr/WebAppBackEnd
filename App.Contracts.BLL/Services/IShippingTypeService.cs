using Base.Contracts.BLL;

namespace App.Contracts.BLL.Services;

public interface IShippingTypeService : IEntityService<App.BLL.DTO.ShippingType>, IShippingTypeRepositoryCustom<App.BLL.DTO.ShippingType>
{

}

public interface IShippingTypeRepositoryCustom<TEntity>
{

}