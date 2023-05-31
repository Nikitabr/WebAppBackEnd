using Base.Contracts.BLL;

namespace App.Contracts.BLL.Services;

public interface IShippingInfoService : IEntityService<App.BLL.DTO.ShippingInfo>, IShippingInfoRepositoryCustom<App.BLL.DTO.ShippingInfo>
{

}

public interface IShippingInfoRepositoryCustom<TEntity>
{

}