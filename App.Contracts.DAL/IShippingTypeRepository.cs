using App.DAL.DTO;
using Base.Contracts.DAL;

namespace App.Contracts.DAL;

public interface IShippingTypeRepository : IEntityRepository<App.DAL.DTO.ShippingType>, IShippingTypeRepositoryCustom<ShippingType>
{
    
}

public interface IShippingTypeRepositoryCustom<TEntity>
{
    // write your custom methods here 
    
    
}