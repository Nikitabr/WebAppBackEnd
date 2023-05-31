using App.DAL.DTO.Identity;
using Base.Contracts.DAL;

namespace App.Contracts.DAL.Identity;

public interface IRefreshTokenRepository : IEntityRepository<App.DAL.DTO.Identity.RefreshToken>, IRefreshTokenRepositoryCustom<RefreshToken>
{
    
}

public interface IRefreshTokenRepositoryCustom<TEntity>
{
    // write your custom methods here 
    Task<IEnumerable<TEntity>> GetRefreshTokensByUser(Guid userId, bool noTracking = true);

}