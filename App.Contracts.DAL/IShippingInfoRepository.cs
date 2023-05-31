using App.DAL.DTO;
using Base.Contracts.DAL;

namespace App.Contracts.DAL;

public interface IShippingInfoRepository : IEntityRepository<App.DAL.DTO.ShippingInfo>, IShippingInfoRepositoryCustom<ShippingInfo>
{
    
}

public interface IShippingInfoRepositoryCustom<TEntity>
{
    // write your custom methods here 
    
    
}